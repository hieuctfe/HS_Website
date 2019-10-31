namespace RealEstate.Areas.Admin.Controllers.Shared
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Infrastructure;
    using RealEstate.CustomAttributes;
    using RealEstate.Models.Identity;

    [AdminAuthorize]
    public abstract class BaseAdminController : Controller
    {
        protected readonly _Converter _converter;

        protected readonly _Context _context;

        protected string _rootUrl => string.Format("{0}://{1}{2}",
                System.Web.HttpContext.Current.Request.Url.Scheme,
                System.Web.HttpContext.Current.Request.Url.Authority,
                Url.Content("~"));

        protected BaseAdminController()
        {
            _context = new _Context();
            _converter = new _Converter();
        }

        #region Images
        protected IEnumerable<_ImageCropper> SaveImages(IEnumerable<_ImageCropper> images)
            => images.Select(x => SaveImage(x));

        protected _ImageCropper SaveImage(_ImageCropper img)
        {
            string _defaultImage = Url.DefaultImage();
            string _imageStorage = Url.ImageStorage();

            if (img == null)
            {
                img = new _ImageCropper(_defaultImage, _imageStorage + _defaultImage);
            }
            else
            {
                if (!img.IsUploaded)
                {
                    string finalName = (img.GetType() == typeof(_ImageCropper) || !string.IsNullOrEmpty(img.ImageData)) ?
                                        Server.MapPath(_imageStorage).WriteBase64ToFile(img.ImageName, img.ImageData.Split(',')[1]) :
                                        _defaultImage;

                    img.ImageName = finalName;
                    img.ImagePath = _imageStorage + finalName;
                }//end if handle case image is uploaded already
            }//end if handle case args is null
            return img;
        }
        #endregion

        #region Districts
        [HttpGet]
        public JsonResult GetDistricts(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                Response.StatusCode = 400;
            }//end if handle case args is not valid

            IQueryable<District> districts = null;
            try
            {
                districts = _context.Districts.Include(x => x.City).Where(x => x.City.Name == cityName).OrderBy(x => x.Name);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(districts.Select(x => x.Name).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RenderPartialViewToString
        [NonAction]
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        [NonAction]
        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        [NonAction]
        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        [NonAction]
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion

        #region Related Data
        [NonAction]
        protected IEnumerable<SelectListItem> GetPropertyTypes(int selected)
            => _context.PropertyTypes.OrderBy(x => x.Name).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == selected
            });

        [NonAction]
        protected IEnumerable<SelectListItem> GetDistricts(string cityName, string districtName)
        {
            if (string.IsNullOrEmpty(cityName))
                return null;

            return _context.Districts.Include(x => x.City).Where(x => x.City.Name == cityName)
                        .OrderBy(x => x.Name).Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Name.ToString(),
                            Selected = x.Name == districtName
                        }).DefaultIfEmpty();
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetCities(string selected)
            => _context.Cities.OrderBy(x => x.Name).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Name == selected
            });

        [NonAction]
        protected IEnumerable<SelectListItem> GetPropertyStatusCodes(int selected)
            => _context.PropertyStatusCodes.OrderBy(x => x.Name).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == selected
            });

        [NonAction]
        protected IEnumerable<SelectListItem> GetCategories(int selected)
            => _context.PostCategories.OrderBy(x => x.Name).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == selected
            });

        [NonAction]
        protected IEnumerable<SelectListItem> GetLabels(IEnumerable<int> selected)
        {
            if (selected == null)
                selected = Enumerable.Empty<int>();

            return _context.PostLabels.OrderBy(x => x.Name).ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selected.Contains(x.Id)
            });
        }
        #endregion

        #region Helper
        protected void AddErrors(string message) => ModelState.AddModelError(string.Empty, message);

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "ManageProperties");
        }
        #endregion
    }
}