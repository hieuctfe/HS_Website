namespace RealEstate
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "BlogList",
                url: "du-an-noi-bat/{statusId}/{typeId}/{data}",
                defaults: new { controller = "Home", action = "Featured", statusId = UrlParameter.Optional, typeId = UrlParameter.Optional, data = UrlParameter.Optional }
                );

            routes.MapRoute(name: "BlogDetails",
                url: "chi-tiet-bai-viet/{title}/{id}",
                defaults: new { controller = "Blog", action = "Detail" }
                );

            routes.MapRoute(name: "PropertyDetails",
                url: "chi-tiet-bat-dong-san/{title}/{id}",
                defaults: new { controller = "Home", action = "Detail" }
                );

            routes.MapRoute(name: "AboutUs",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}