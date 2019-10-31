namespace RealEstate.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using RealEstate.Models;
    using RealEstate.Models.UI;
    using RealEstate.ViewModels;
    using RealEstate.ViewModels.UI;
    using RealEstate.Controllers.Shared;

    public class HomeController : BaseController
    {
        [Route("trang-chu")]
        public ActionResult Index()
        {
            IQueryable<Post> posts = _context.Posts.Include(x => x.Comments).Where(x => x.UIOption.IsDisplay);
            IQueryable<Property> properties = _context.Properties.Include(x => x.PropertyStatusCode)
                                                .Include(x => x.PropertyType).Where(x => x.UIOption.IsDisplay);

            List<Property> recentProperties = properties.OrderByDescending(x => x.ActivityLog.CreatedOn)
                .Take(9).ToList();
            List<Property> featuredProperties = properties.OrderByDescending(x => x.UIOption.DisplayOrder)
                .Take(4).ToList();
            List<Post> recentPosts = posts.OrderByDescending(x => x.ActivityLog.CreatedOn)
                .Take(3).ToList();

            UICache banner = _context.UICaches.Find(Page.Home, PageComponent.Home_Slider, DataType.Html);
            UICache offer = _context.UICaches.Find(Page.Home, PageComponent.Home_Offer, DataType.Html);
            UICache service = _context.UICaches.Find(Page.Home, PageComponent.Home_Service, DataType.Html);
            UICache achivement = _context.UICaches.Find(Page.Home, PageComponent.Home_Achivement, DataType.Html);
            UICache team = _context.UICaches.Find(Page.Home, PageComponent.Home_Team, DataType.Html);
            UICache client = _context.UICaches.Find(Page.Home, PageComponent.Home_Client, DataType.Html);

            ViewBag.SortList = GetSortFields();
            ViewBag.Statuses = GetPropertyStatusCodes(-1);
            ViewBag.Types = GetPropertyTypes(-1);
            ViewBag.Cities = GetCities(null);
            ViewBag.Districts = GetDistricts(null, null);

            return View(new HomePageViewModels
            {
                BannerHtml = banner == null ? string.Empty : banner.DataCache,
                OfferHtml = offer == null ? string.Empty : offer.DataCache,
                ServiceHtml = service == null ? string.Empty : service.DataCache,
                AchivementHtml = achivement == null ? string.Empty : achivement.DataCache,
                TeamHtml = team == null ?  string.Empty : team.DataCache,
                ClientHtml = client == null ?  string.Empty : client.DataCache,

                RecentProperties = _converter.CreatePropertyViewModels(recentProperties).ToList(),
                FeaturedProperties = _converter.CreatePropertyViewModels(featuredProperties).ToList(),
                
                RecentPosts = _converter.CreatePostViewModels(recentPosts).ToList(),
            });
        }
    }
}