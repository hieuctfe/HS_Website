namespace RealEstate.CustomAttributes
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            context.Result = new RedirectToRouteResult("Admin_default",
                    new RouteValueDictionary{
                        { "controller", "ManageAccount" },
                        { "action", "Login" },
                        { "returnUrl", context.HttpContext.Request.RawUrl }
                    });
        }
    }
}