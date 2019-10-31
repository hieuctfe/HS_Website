[assembly: Microsoft.Owin.OwinStartup(typeof(RealEstate.Startup))]
namespace RealEstate
{
    using Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.AspNet.Identity;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/admin/manageaccount/login")
            });
        }
    }
}
