using RealEstate.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class ContactController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.BannerImage = base.BreadCumImage;

            return View();
        }

        [HttpGet]
        public ActionResult SubmitProperty()
        {
            return View();
        }
    }
}