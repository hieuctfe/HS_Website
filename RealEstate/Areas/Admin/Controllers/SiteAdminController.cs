namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Areas.Admin.Controllers.Shared;
    using RealEstate.ViewModels.UI;
    using RealEstate.App_Start;
    using RealEstate.Models.Identity;

    public class SiteAdminController : BaseAdminController
    {
        private readonly AccountService _accountService;

        public SiteAdminController()
        {
            _accountService = new AccountService(new UserStore<Account>(_context));
        }

        [ChildActionOnly]
        public PartialViewResult RenderTopBar()
        {
            Account user = _accountService.FindById(User.Identity.GetUserId());

            return PartialView("LayoutComponents/03_Body_MW_Topbar", _converter.CreateProfileViewModels(user));
        }

        [ChildActionOnly]
        public PartialViewResult RenderLeftSidebar()
        {
            Account user = _accountService.FindById(User.Identity.GetUserId());

            return PartialView("LayoutComponents/04_Body_MW_LeftSidebar", _converter.CreateProfileViewModels(user));
        }

        #region Paging
        [ChildActionOnly]
        public PartialViewResult RenderPaging(_PageView pageView)
        {
            new Dictionary<string, object> { ["Name"] = pageView };
            int leftPage, rightPage,
                startPage = 1, endPage = pageView.TotalPage;

            pageView.PageIndex = pageView.PageIndex < 1 ? 1 : pageView.PageIndex > endPage ? endPage : pageView.PageIndex;

            bool isOdd = pageView.ShowPage % 2 != 0;
            if (isOdd)
            {
                leftPage = rightPage = (int)Math.Floor((double)pageView.ShowPage / 2);
            }
            else
            {
                rightPage = (int)Math.Floor((double)pageView.ShowPage / 2);
                leftPage = rightPage - 1;
            }

            if (pageView.PageIndex <= (startPage + leftPage))
            {
                endPage = startPage + (pageView.ShowPage - (isOdd ? 1 : 0));
            }
            else if (pageView.PageIndex >= (endPage - rightPage))
            {
                startPage = endPage - (pageView.ShowPage - 1);
            }
            else
            {
                startPage = pageView.PageIndex - leftPage;
                endPage = pageView.PageIndex + rightPage;
            }

            pageView.StartPage = startPage < 1 ? 1 : startPage;
            pageView.EndPage = endPage > pageView.TotalPage ? pageView.TotalPage : endPage;
            return PartialView("ViewComponents/_Pagination", pageView);
        }
        #endregion

        #region RenderForm
        [HttpGet]
        public JsonResult RenderHomeClientForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_ClientElement.cshtml",
                new ClientViewModels()),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RenderHomeTeamForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_TeamElement.cshtml",
                new HomeUITeamViewModels()),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RenderHomeBannerForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_BannerElement.cshtml", 
                new HomeUIBannerViewModels()), 
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RenderHomeOfferForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_OfferElement.cshtml",
                new HomeUIOfferViewModels()),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RenderHomeServiceForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_ServiceElement.cshtml",
                new ServiceViewModels()),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RenderHomeAchivementForm()
        {
            return Json(RenderPartialViewToString("~/Areas/Admin/Views/ManageHomePageUI/_AchivementElement.cshtml",
                new AchivementViewModels()),
                JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Post Attribute
        [HttpGet]
        public JsonResult CategoryManagement()
        {
            List<PostCategory> categories = _context.PostCategories.OrderBy(x => x.Name).ToList();

            string modalForm = "~/Areas/Admin/Views/ManageBlog/_ModalFormTemplate.cshtml";

            return Json(RenderPartialViewToString(modalForm, 
                new ManagePostAttributeViewModels
                {
                    ModalName = "DANH SÁCH DANH MỤC",
                    Item = categories.Select(x => (x.Id, x.Name))
                }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LabelManagement()
        {
            List<PostLabel> labels = _context.PostLabels.OrderBy(x => x.Name).ToList();

            string modalForm = "~/Areas/Admin/Views/ManageBlog/_ModalFormTemplate.cshtml";

            return Json(RenderPartialViewToString(modalForm,
                new ManagePostAttributeViewModels
                {
                    ModalName = "DANH SÁCH TAGS",
                    Item = labels.Select(x => (x.Id, x.Name))
                }), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}