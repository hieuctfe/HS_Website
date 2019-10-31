namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using RealEstate.Models;
    using RealEstate.Areas.Admin.Controllers.Shared;

    public class ManageBlogAttributeController : BaseAdminController
    {
        #region Create - Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateCategory(string name)
        {
            try
            {
                var category = new PostCategory { Name = name };

                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    _context.PostCategories.Add(category);
                    _context.SaveChanges();

                    transaction.Commit();
                }//end transaction

                return Json(new { category.Id, category.Name });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty);
        }
        #endregion

        #region Delete - Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCategory(int[] ids)
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
                        ids.ToList().ForEach(id => _context.Entry(new PostCategory { Id = id }).State = EntityState.Deleted);
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

        #region Create - Label
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateLabel(string name)
        {
            try
            {
                var label = new PostLabel { Name = name };

                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    _context.PostLabels.Add(label);
                    _context.SaveChanges();

                    transaction.Commit();
                }//end transaction

                return Json(new { label.Id, label.Name });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            return Json(string.Empty);
        }
        #endregion

        #region Delete - Label
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteLabel(int[] ids)
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
                        ids.ToList().ForEach(id => _context.Entry(new PostLabel { Id = id }).State = EntityState.Deleted);
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
    }
}