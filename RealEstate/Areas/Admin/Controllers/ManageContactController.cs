namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using RealEstate.Areas.Admin.Controllers.Shared;
    using RealEstate.Infrastructure;
    using RealEstate.Models;
    using RealEstate.ViewModels;

    public class ManageContactController : BaseAdminController
    {
        [HttpGet]
        public ActionResult Index(int? typeId, _Pager pager)
        {
            IQueryable<Comment> comments = _context.Comments;

            if (typeId.HasValue)
            {
                switch (typeId.Value)
                {
                    case 1:
                        comments = comments.Where(x => x.PropertyId.HasValue);
                        break;
                    case 2:
                        comments = comments.Where(x => x.PostId.HasValue);
                        break;
                }
            }

            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string sortFieldValue = $"{nameof(Comment.Owner)}";//default order field
            string orderByValue = "asc";//default order by

            if (!string.IsNullOrEmpty(pager.SortField))
            {
                string[] segments = pager.SortField.Split('_');

                sortFieldValue = segments[0];
                orderByValue = segments[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            int totalPages = (int)Math.Ceiling(comments.Count() / (double)pageSize);

            ViewBag.SortField = pager.SortField;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchValue = typeId;
            return View(_converter.CreateReadCommentViewModels(comments.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList(), _rootUrl));
        }

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int[] ids)
        {
            try
            {
                if (!Request.IsAjaxRequest() || ids == null || ids.Length == 0)
                {
                    Response.StatusCode = 400;
                }
                else
                {
                    using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                    {
                        ids.ToList().ForEach(id => _context.Entry(new Comment { Id = id }).State = EntityState.Deleted);

                        _context.SaveChanges();
                        transaction.Commit();
                    }//end transaction
                }//end if handle case args is not valid
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty);
        }
        #endregion

        #region Update UI state
        [HttpGet]
        public JsonResult Display(int? id)
        {
            try
            {
                if (!Request.IsAjaxRequest() || !id.HasValue)
                {
                    Response.StatusCode = 400;
                }
                else
                {
                    Comment comment = _context.Comments.FirstOrDefault(x => x.Id == id.Value);

                    if (comment != null)
                    {
                        using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                        {
                            comment.IsVerify = !comment.IsVerify;

                            _context.SaveChanges();
                            transaction.Commit();
                        }//end transaction
                    }
                    else
                    {
                        Response.StatusCode = 400;
                    }//end if handle case property is not existed
                }//end if handle case args is not valid
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}