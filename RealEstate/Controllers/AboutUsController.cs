using RealEstate.Controllers.Shared;
using RealEstate.Models.UI;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class AboutUsController : BaseController
    {
        public ActionResult Index()
        {
            UICache client = _context.UICaches.Find(Page.Home, PageComponent.Home_AboutUs, DataType.Html);
            if (client == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            return View(client);
        }
    }
}