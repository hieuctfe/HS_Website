namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Infrastructure;
    using RealEstate.Areas.Admin.Controllers.Shared;

    public class ManagePropertiesController : BaseAdminController
    {
        #region Index
        [HttpGet]
        public ActionResult Index(_Pager pager)
        {
            IQueryable<Property> properties = _context.Properties
                                            .Include(x => x.PropertyStatusCode)
                                            .Include(x => x.PropertyType);

            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string searchValue = pager.SearchValue,
                sortFieldValue = $"{nameof(Property.Name)}";//default order field
            string orderByValue = "asc";//default order by

            if (!string.IsNullOrEmpty(pager.SortField))
            {
                string[] segments = pager.SortField.Split('_');

                sortFieldValue = segments[0];
                orderByValue = segments[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            properties = properties.Where(x => string.IsNullOrEmpty(searchValue)
                                                                    || x.Name.Contains(searchValue));

            int totalPages = (int)Math.Ceiling(properties.Count() / (double)pageSize);

            ViewBag.SortField = pager.SortField;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchValue = searchValue;

            return View(_converter.CreatePropertyViewModels(properties.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList()));
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PropertyTypes = GetPropertyTypes(-1);
            ViewBag.PropertyStatusCodes = GetPropertyStatusCodes(-1);
            ViewBag.Cities = GetCities(string.Empty);

            return View(new CreatePropertyViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePropertyViewModels property)
        {
            ViewBag.PropertyTypes = GetPropertyTypes(property.BasicInformation.PropertyTypeId);
            ViewBag.PropertyStatusCodes = GetPropertyStatusCodes(property.BasicInformation.PropertyTypeId);
            ViewBag.Cities = GetCities(property.BasicInformation.City);
            ViewBag.Districts = GetDistricts(property.BasicInformation.City, property.BasicInformation.District);

            if (ModelState.IsValid)
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    Property newProperty = _converter.CreateProperty(property);

                    _ImageCropper avatar = SaveImage(property.Avatar.FirstOrDefault());
                    newProperty.AvatarName = avatar.ImageName;
                    newProperty.AvatarPath = avatar.ImagePath;

                    SaveImages(property.PropertyImages).ToList().ForEach(x =>
                    {
                        newProperty.PropertyImages.Add(new PropertyImage
                        {
                            Name = x.ImageName,
                            Path = x.ImagePath
                        });
                    });

                    _context.Entry(newProperty).State = EntityState.Added;
                    _context.SaveChanges();

                    transaction.Commit();
                }//end transaction

                return RedirectToAction(nameof(Index));
            }//end if handle case model is not valid

            return View(property);
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Property property = _context.Properties.Include(x => x.PropertyImages)
                                                       .FirstOrDefault(x => x.Id == id.Value);

                if (property != null)
                {
                    ViewBag.PropertyTypes = GetPropertyTypes(property.PropertyTypeId);
                    ViewBag.PropertyStatusCodes = GetPropertyStatusCodes(property.PropertyStatusCodeId);
                    ViewBag.Cities = GetCities(property.City);
                    ViewBag.Districts = GetDistricts(property.City, property.District);

                    return View(_converter.CreateUpdatePropertyViewModels(property));
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }//end if handle case property is not existed
            }//end if handle case args is null

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? id, UpdatePropertyViewModels property)
        {
            if (id.HasValue && ModelState.IsValid)
            {
                Property baseProperty = _context.Properties.Include(x => x.PropertyImages)
                                                    .FirstOrDefault(x => x.Id == id.Value);
                if (baseProperty != null)
                {
                    using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                    {
                        _converter.UpdateProperty(property, baseProperty);

                        _ImageCropper avatar = SaveImage(property.Avatar.FirstOrDefault());
                        baseProperty.AvatarName = avatar.ImageName;
                        baseProperty.AvatarPath = avatar.ImagePath;

                        List<PropertyImage> baseImages = baseProperty.PropertyImages.ToList();
                        baseImages.ForEach(x => _context.Entry(x).State = EntityState.Deleted);

                        SaveImages(property.PropertyImages).ToList().ForEach(x =>
                        {
                            if (x.IsUploaded)
                            {
                                PropertyImage baseImage = baseImages.FirstOrDefault(y => y.Path == x.ImagePath);
                                _context.Entry(baseImage).State = EntityState.Unchanged;
                            }
                            else
                            {
                                baseProperty.PropertyImages.Add(new PropertyImage
                                {
                                    Name = x.ImageName,
                                    Path = x.ImagePath,
                                });
                            }//end if handle case image is modified or not
                        });//end for upload and write image to physical file

                        _context.SaveChanges();
                        transaction.Commit();
                    }//end transaction

                    return RedirectToAction(nameof(Index));
                }//end if handle case model is not valid
            }//end if handle case args is null

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(List<int> ids)
        {
            try
            {
                if (!Request.IsAjaxRequest() || ids == null || ids.Count == 0)
                {
                    Response.StatusCode = 400;
                }
                else
                {
                    using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                    {
                        ids.ForEach(id => _context.Entry(new Property
                        {
                            Id = id
                        }).State = EntityState.Deleted);

                        _context.SaveChanges();
                        transaction.Commit();
                    }//end transaction
                }//end if handle case args is not valid
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty);
        }
        #endregion

        #region Update UI state
        [HttpGet]
        public JsonResult Display(int? id)
        {
            try
            {
                if (!Request.IsAjaxRequest() || !id.HasValue)
                {
                    Response.StatusCode = 400;
                }
                else
                {
                    Property property = _context.Properties.FirstOrDefault(x => x.Id == id.Value);

                    if (property != null)
                    {
                        using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                        {
                            property.UIOption.IsDisplay = !property.UIOption.IsDisplay;

                            _context.SaveChanges();
                            transaction.Commit();
                        }//end transaction
                    }
                    else
                    {
                        Response.StatusCode = 404;
                    }//end if handle case property is not existed
                }//end if handle case args is not valid
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Featured(int? id)
        {
            try
            {
                if (!Request.IsAjaxRequest() || !id.HasValue)
                {
                    Response.StatusCode = 400;
                }
                else
                {
                    Property property = _context.Properties.FirstOrDefault(x => x.Id == id.Value);

                    if (property != null)
                    {
                        using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                        {
                            property.IsFeatured = !property.IsFeatured;

                            _context.SaveChanges();
                            transaction.Commit();
                        }//end transaction
                    }
                    else
                    {
                        Response.StatusCode = 404;
                    }//end if handle case property is not existed
                }//end if handle case args is not valid
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}