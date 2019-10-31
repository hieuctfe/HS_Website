namespace RealEstate.Infrastructure
{
    using System.Web.Mvc;
    using RealEstate.App_Start;

    public static class UrlExtension
    {
        private static string _userAvatar = string.Empty;

        public static string AppAssets(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Admin_App_Path_Assets}{contentPath}");

        public static string AppCustoms(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Admin_App_Path_Customs}{contentPath}");

        public static string AppVendors(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Admin_App_Path_Vendors}{contentPath}");

        public static string DefaultImage(this UrlHelper urlHelper) =>
            urlHelper.Content($"{SiteConfig.Admin_App_Default_Images}");

        public static string ImageStorage(this UrlHelper urlHelper) =>
            urlHelper.Content($"{SiteConfig.Admin_App_Image_Storage}");

        public static string ClientAppAssets(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Client_App_Path_Assets}{contentPath}");

        public static string ClientCustoms(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Client_App_Path_Customs}{contentPath}");

        public static string ClientAppVendors(this UrlHelper urlHelper, string contentPath) =>
            urlHelper.Content($"{SiteConfig.Client_App_Path_Vendors}{contentPath}");
    }
}