namespace RealEstate.App_Start
{
    using System.Web.Configuration;

    public class SiteConfig
    {
        public readonly static string Admin_App_Path_Assets = WebConfigurationManager.AppSettings["Admin.App.Path.Assets"];
        public readonly static string Admin_App_Path_Customs = WebConfigurationManager.AppSettings["Admin.App.Path.Customs"];
        public readonly static string Admin_App_Path_Vendors = WebConfigurationManager.AppSettings["Admin.App.Path.Vendors"];
        public readonly static string Admin_App_Route_Prefix = WebConfigurationManager.AppSettings["Admin.App.Route.Prefix"];
        public readonly static string Admin_App_Image_Storage = WebConfigurationManager.AppSettings["Admin.App.Image.Storage"];
        public readonly static string Admin_App_Default_Images = WebConfigurationManager.AppSettings["Admin.App.Image.Default"];

        public readonly static string Client_App_Path_Assets = WebConfigurationManager.AppSettings["Client.App.Path.Assets"];
        public readonly static string Client_App_Path_Customs = WebConfigurationManager.AppSettings["Client.App.Path.Customs"];
        public readonly static string Client_App_Path_Vendors = WebConfigurationManager.AppSettings["Client.App.Path.Vendors"];
        public readonly static string Client_App_Route_Prefix = WebConfigurationManager.AppSettings["Client.App.Route.Prefix"];
    }
}