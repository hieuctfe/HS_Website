namespace RealEstate.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using RealEstate.Areas.Admin.Controllers.Shared;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.ViewModels.UI;
    using RealEstate.Infrastructure;
    using System.Data.Entity;
    using System;

    public class ManageMenuController : BaseAdminController
    {
        #region Index
        [HttpGet]
        public ActionResult Index()
        {
            return View(new PropertyMenuViewModels
            {
                PropertyStatusCode = _context.PropertyStatusCodes.OrderBy(x => x.UIOption.DisplayOrder).ToList()
                .Select(x => new UpdateMenuItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    UIOption = x.UIOption
                }),
                PropertyTypes = _context.PropertyTypes.OrderBy(x => x.UIOption.DisplayOrder).ToList()
                .Select(x => new UpdateMenuItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    UIOption = x.UIOption
                }),
                BlogProperty = _context.PostCategories.OrderBy(x => x.UIOption.DisplayOrder).ToList()
                .Select(x => new UpdateMenuItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    UIOption = x.UIOption
                })
            });
        }
        #endregion

        #region Create/Delete PropertyType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePropertyType(string name)
        {
            try
            {
                PropertyType createType = new PropertyType { Name = name };

                _context.Entry(createType).State = EntityState.Added;
                _context.SaveChanges();

                return Json(createType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePropertyType(int id)
        {
            try
            {
                _context.Entry(new PropertyType { Id = id }).State = EntityState.Deleted;
                _context.SaveChanges();

                return Json(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create/Delete PropertyStatusCode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePropertyStatusCode(string name)
        {
            try
            {
                PropertyStatusCode createStatusCode = new PropertyStatusCode { Name = name };

                _context.Entry(createStatusCode).State = EntityState.Added;
                _context.SaveChanges();

                return Json(createStatusCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePropertyStatusCode(int id)
        {
            try
            {
                _context.Entry(new PropertyStatusCode { Id = id }).State = EntityState.Deleted;
                _context.SaveChanges();

                return Json(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create/Delete BlogType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateBlogType(string name)
        {
            try
            {
                PostCategory createType = new PostCategory { Name = name };

                _context.Entry(createType).State = EntityState.Added;
                _context.SaveChanges();

                return Json(createType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteBlogType(int id)
        {
            try
            {
                _context.Entry(new PostCategory { Id = id }).State = EntityState.Deleted;
                _context.SaveChanges();

                return Json(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdatePosition
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePosition(IEnumerable<int> propertyTypes, IEnumerable<int> propertyStatusCodes, 
            IEnumerable<int> blogTypes)
        {
            try
            {
                propertyTypes.ForEach((i, x) =>
                {
                    var temp = _context.PropertyTypes.Find(x);
                    if (temp != null)
                        temp.UIOption.DisplayOrder = i;
                });

                propertyStatusCodes.ForEach((i, x) =>
                {
                    var temp = _context.PropertyStatusCodes.Find(x);
                    if (temp != null)
                        temp.UIOption.DisplayOrder = i;
                });

                blogTypes.ForEach((i, x) =>
                {
                    var temp = _context.PostCategories.Find(x);
                    if (temp != null)
                        temp.UIOption.DisplayOrder = i;
                });

                
                _context.SaveChanges();
                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}