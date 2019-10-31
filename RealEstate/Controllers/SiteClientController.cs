namespace RealEstate.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using RealEstate.ViewModels;
    using RealEstate.Controllers.Shared;
    using RealEstate.Models.UI;
    using RealEstate.ViewModels.UI;

    public class SiteClientController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult RenderFooter()
        {
            return PartialView("LayoutComponents/05_Footer_Section", new HomeUIFooteViewModels
            {
                FooterHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Footer, DataType.Html).DataCache
            });
        }

        #region Dynamic Menu
        [ChildActionOnly]
        public PartialViewResult RenderPropertyTypeMenu()
        {
            return PartialView("ViewComponents/_DynamicMenu",
                _context.PropertyTypes.OrderBy(x => x.UIOption.DisplayOrder).ToList().Select(x => new _MenuItem
                {
                    Name = x.Name,
                    Url = Url.Action("Index", "Home", new
                    {
                        typeId = x.Id
                    })
                }));
        }

        [ChildActionOnly]
        public PartialViewResult RenderPropertyStatusMenu()
        {
            return PartialView("ViewComponents/_DynamicMenu",
                _context.PropertyStatusCodes.OrderBy(x => x.UIOption.DisplayOrder).ToList().Select(x => new _MenuItem
                {
                    Name = x.Name,
                    Url = Url.Action("Index", "Home", new
                    {
                        statusId = x.Id
                    })
                }));
        }

        [ChildActionOnly]
        public PartialViewResult RenderCategoryMenu()
        {
            return PartialView("ViewComponents/_DynamicMenu", 
                _context.PostCategories.OrderBy(x => x.UIOption.DisplayOrder).ToList().Select(x => new _MenuItem
                {
                    Name = x.Name,
                    Url = Url.Action("Index", "Blog", new
                    {
                        categoryId = x.Id
                    })
                }));
        }

        [ChildActionOnly]
        public PartialViewResult RenderLabelMenu()
        {
            return PartialView("ViewComponents/_DynamicMenu",
                _context.PostLabels.OrderBy(x => x.UIOption.DisplayOrder).ToList().Select(x => new _MenuItem
                {
                    Name = x.Name,
                    Url = Url.Action("Index", "Blog", new
                    {
                        labelId = x.Id
                    })
                }));
        }
        #endregion

        #region Paging
        [ChildActionOnly]
        public PartialViewResult RenderPaging(_PageView pageView)
        {
            new Dictionary<string, object>()
            {
                ["Name"] = pageView,

            };
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

        #region RenderPartialViewToString
        /*
        Tham khao https://craftycode.wordpress.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        */
        [NonAction]
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        [NonAction]
        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        [NonAction]
        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        [NonAction]
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}