namespace RealEstate
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "CustomResource";
            DefaultModelBinder.ResourceClassKey = "CustomResource";

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
