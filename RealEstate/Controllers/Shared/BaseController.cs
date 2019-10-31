namespace RealEstate.Controllers.Shared
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Models.UI;
    using RealEstate.Infrastructure;

    public abstract class BaseController : Controller
    {
        private string _breadCumImage = string.Empty;
        protected readonly _Converter _converter;
        protected readonly _Context _context;

        protected string _rootUrl => string.Format("{0}://{1}{2}",
                System.Web.HttpContext.Current.Request.Url.Scheme,
                System.Web.HttpContext.Current.Request.Url.Authority,
                Url.Content("~"));

        protected BaseController()
        {
            _converter = new _Converter();
            _context = new _Context();
        }

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

        protected string BreadCumImage
        {
            get
            {
                if (string.IsNullOrEmpty(_breadCumImage))
                {
                    UICache bannerImage = _context.UICaches.Find(Page.Home, PageComponent.Home_Banner, DataType.Field);
                    if (bannerImage != null)
                        _breadCumImage = bannerImage.DataCache;
                }

                return _breadCumImage;
            }
        }

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
                districts = _context.Districts.Include(x => x.City).Where(x => x.City.Name == cityName);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(districts.Select(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        #region Related data
        [NonAction]
        protected IEnumerable<SelectListItem> GetSortFields()
            => new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Ngày Đăng",
                    Value = $"{nameof(Property.ActivityLog)}." +
                            $"CreatedOn_desc"
                },
                new SelectListItem
                {
                    Text = "Giá Tăng Dần",
                    Value = $"{nameof(Property.Price)}_asc"
                },
                new SelectListItem
                {
                    Text = "Giá Giảm Dần",
                    Value = $"{nameof(Property.Price)}_desc"
                }
            };

        [NonAction]
        protected IEnumerable<SelectListItem> GetPropertyTypes(int selected)
            => _context.PropertyTypes.ToList().Select(x => new SelectListItem
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
                        .Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Name.ToString(),
                            Selected = x.Name == districtName
                        }).DefaultIfEmpty();
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetCities(string selected)
            => _context.Cities.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Name == selected
            });

        [NonAction]
        protected IEnumerable<SelectListItem> GetPropertyStatusCodes(int selected)
            => _context.PropertyStatusCodes.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == selected
            });
        #endregion
    }
}