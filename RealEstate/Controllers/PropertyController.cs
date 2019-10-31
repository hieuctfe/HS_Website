namespace RealEstate.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Infrastructure;
    using RealEstate.Controllers.Shared;
    using RealEstate.Models.UI;

    public class PropertyController : BaseController
    {
        #region Index
        [HttpGet]
        public ActionResult Index(int? statusId, int? typeId, PropertyPageViewModels data)
        {
            ViewBag.BannerImage = base.BreadCumImage;

            IQueryable<Property> properties = _context.Properties
                .Include(x => x.PropertyStatusCode).Include(x => x.PropertyType)
                .Where(x => x.UIOption.IsDisplay);
            
            var pager = data.PropertyPagination;
            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string sortFieldValue = $"{nameof(Property.Name)}";//default order field;
            string orderByValue = "asc";//default order by

            if (pager.IsSort)
            {
                sortFieldValue = pager.SortField;

                string[] segmentSort = sortFieldValue.Split('_');

                sortFieldValue = segmentSort[0];
                orderByValue = segmentSort[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            if (pager.IsSearch || statusId.HasValue || typeId.HasValue)
            {
                int.TryParse(pager.Bed, out int bedValue);
                int.TryParse(pager.Bath, out int bathValue);

                properties = properties.Where(x =>
                    (statusId.HasValue ? x.PropertyStatusCodeId == statusId : 
                    string.IsNullOrEmpty(pager.Status) || x.PropertyStatusCodeId.ToString() == pager.Status) &&
                    (typeId.HasValue ? x.PropertyTypeId == typeId : 
                    (string.IsNullOrEmpty(pager.Type) || x.PropertyTypeId.ToString() == pager.Type)) &&
                    (string.IsNullOrEmpty(pager.Bed)  || x.NumberOfBedRoom >= bedValue) &&
                    (string.IsNullOrEmpty(pager.Bath) || x.NumberOfBathRoom >= bathValue) &&
                    (string.IsNullOrEmpty(pager.City) || x.City == pager.City) &&
                    (string.IsNullOrEmpty(pager.District) || x.District == pager.District));
            }//search property for status, type, bath, bed, city, district and init value for districts

            ViewBag.SortList = GetSortFields();
            ViewBag.Statuses = GetPropertyStatusCodes(-1);
            ViewBag.Types = GetPropertyTypes(-1);
            ViewBag.Cities = GetCities(pager.City);
            ViewBag.Districts = GetDistricts(pager.City, pager.District);

            string[] segmentArea = pager.AreaValue.Split(';');
            decimal fromAreaValue = decimal.Parse(segmentArea[0]);
            decimal toAreaValue = decimal.Parse(segmentArea[1]);

            properties = properties.Where(x => fromAreaValue <= x.Area && x.Area <= (toAreaValue == 1000 ? decimal.MaxValue : toAreaValue) &&
                                        (pager.FromPriceValue ?? 0) <= x.Price && x.Price <= (pager.ToPriceValue ?? decimal.MaxValue));
            //search property for price and area value

            int totalPages = (int)Math.Ceiling(properties.Count() / (double)pageSize);

            IQueryable<Post> posts = _context.Posts.Include(x => x.Comments).Where(x => x.UIOption.IsDisplay);

            var recentPost = posts.OrderByDescending(x => x.ActivityLog.CreatedOn).Take(4).ToList();

            ViewBag.RecentPosts = _converter.CreatePostViewModels(recentPost);
            ViewBag.SortField = $"{sortFieldValue}_{(orderByValue == "asc" ? "asc" : "desc")}";
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            UICache team = _context.UICaches.Find(Page.Property, PageComponent.Home_Team, DataType.Html);
            ViewBag.TeamUI = team == null ? string.Empty : team.DataCache;

            return View(new PropertyPageViewModels
            {
                Properties = _converter.CreatePropertyViewModels(properties.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList()),
                Featured = _converter.CreatePropertyViewModels
                                    (_context.Properties.Where(x => x.IsFeatured).OrderBy(x => Guid.NewGuid()).Take(4).ToList()),
                PropertyPagination = pager
            });
        }

        [HttpGet]
        public ActionResult Featured(int? statusId, int? typeId, PropertyPageViewModels data)
        {
            ViewBag.BannerImage = base.BreadCumImage;

            IQueryable<Property> properties = _context.Properties
                .Include(x => x.PropertyStatusCode).Include(x => x.PropertyType)
                .Where(x => x.UIOption.IsDisplay && x.IsFeatured);

            var pager = data.PropertyPagination;
            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string sortFieldValue = $"{nameof(Property.Name)}";//default order field;
            string orderByValue = "asc";//default order by

            if (pager.IsSort)
            {
                sortFieldValue = pager.SortField;

                string[] segmentSort = sortFieldValue.Split('_');

                sortFieldValue = segmentSort[0];
                orderByValue = segmentSort[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            if (pager.IsSearch || statusId.HasValue || typeId.HasValue)
            {
                properties = properties.Where(x =>
                    (statusId.HasValue ? x.PropertyStatusCodeId == statusId :
                    string.IsNullOrEmpty(pager.Status) || x.PropertyStatusCodeId.ToString() == pager.Status) &&
                    (typeId.HasValue ? x.PropertyTypeId == typeId :
                    (string.IsNullOrEmpty(pager.Type) || x.PropertyTypeId.ToString() == pager.Type)) &&
                    (string.IsNullOrEmpty(pager.Bed) || x.NumberOfBedRoom.ToString() == pager.Bed) &&
                    (string.IsNullOrEmpty(pager.Bath) || x.NumberOfBathRoom.ToString() == pager.Bath) &&
                    (string.IsNullOrEmpty(pager.City) || x.City == pager.City) &&
                    (string.IsNullOrEmpty(pager.District) || x.District == pager.District));
            }//search property for status, type, bath, bed, city, district and init value for districts

            ViewBag.SortList = GetSortFields();
            ViewBag.Statuses = GetPropertyStatusCodes(-1);
            ViewBag.Types = GetPropertyTypes(-1);
            ViewBag.Cities = GetCities(pager.City);
            ViewBag.Districts = GetDistricts(pager.City, pager.District);

            string[] segmentArea = pager.AreaValue.Split(';');
            decimal fromAreaValue = decimal.Parse(segmentArea[0]);
            decimal toAreaValue = decimal.Parse(segmentArea[1]);

            properties = properties.Where(x => fromAreaValue <= x.Area && x.Area <= (toAreaValue == 1000 ? decimal.MaxValue : toAreaValue) &&
                                        (pager.FromPriceValue ?? 0) <= x.Price && x.Price <= (pager.ToPriceValue ?? decimal.MaxValue));
            //search property for price and area value

            int totalPages = (int)Math.Ceiling(properties.Count() / (double)pageSize);
            IQueryable<Post> posts = _context.Posts.Include(x => x.Comments).Where(x => x.UIOption.IsDisplay);

            var recentPost = posts.OrderByDescending(x => x.ActivityLog.CreatedOn).Take(4).ToList();

            ViewBag.RecentPosts = _converter.CreatePostViewModels(recentPost);
            ViewBag.SortField = $"{sortFieldValue}_{(orderByValue == "asc" ? "asc" : "desc")}";
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            UICache team = _context.UICaches.Find(Page.Property, PageComponent.Home_Team, DataType.Html);
            ViewBag.TeamUI = team == null ? string.Empty : team.DataCache;
            return View($"{nameof(Index)}", new PropertyPageViewModels
            {
                Properties = _converter.CreatePropertyViewModels(properties.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList()),
                Featured = _converter.CreatePropertyViewModels
                                    (_context.Properties.Where(x => x.IsFeatured).OrderBy(x => Guid.NewGuid()).Take(4).ToList()),
                PropertyPagination = pager
            });
        }
        #endregion

        #region Detail
        [HttpGet]
        public ActionResult Detail(int? id, string title)
        {
            if (id.HasValue)
            {
                ViewBag.BannerImage = base.BreadCumImage;

                Property property = _context.Properties
                                            .Include(x => x.Comments)
                                            .Include(x => x.PropertyImages)
                                            .Include(x => x.PropertyStatusCode)
                                            .Include(x => x.PropertyType)
                                            .FirstOrDefault(x => x.Id == id.Value);

                ViewBag.Cities = GetCities(null);
                ViewBag.Statuses = GetPropertyStatusCodes(-1);
                ViewBag.Types = GetPropertyTypes(-1);

                string friendlyUrl = property.Name.RewriteUrl(maxLength: 5000);
                if (!string.Equals(friendlyUrl, title, StringComparison.Ordinal))
                {
                    return this.RedirectToRoutePermanent("PropertyDetails", new { id = property.Id, title = friendlyUrl });
                }
                if (property != null)
                    return View(new PropertyDetailPageViewModels
                    {
                        DetailInformation = _converter.CreatePropertyDetailViewModels(property),
                        Featured = _converter.CreatePropertyViewModels
                                    (_context.Properties.Where(x => x.IsFeatured && 
                                                            x.Id != id).OrderBy(x => Guid.NewGuid()).Take(4).ToList()),
                        Related = _converter.CreatePropertyViewModels
                                    (_context.Properties.Where(x => x.PropertyTypeId == property.PropertyTypeId &&
                                                            x.Id != id).OrderBy(x => Guid.NewGuid()).Take(9).ToList()),
                    });
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Submit Property
        [HttpGet]
        public ActionResult SubmitProperty()
        {
            ViewBag.PropertyTypes = GetPropertyTypes(-1);
            ViewBag.PropertyStatusCodes = GetPropertyStatusCodes(-1);
            ViewBag.Cities = GetCities(string.Empty);

            return View(new CreatePropertyViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitProperty(CreatePropertyViewModels property)
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
    }
}