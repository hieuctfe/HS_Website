namespace RealEstate.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Data.Entity;
    using RealEstate.ViewModels;
    using RealEstate.CustomAttributes;
    using RealEstate.Controllers.Shared;

    public class CommentController : BaseController
    {
        [HttpPost, PostModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(CommentViewModels comment)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    _context.Comments.Add(_converter.CreateComment(comment));
                    _context.SaveChanges();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = ex.Message;
            }

            var isBlog = comment.PropertyId == null;
            return RedirectToAction("Detail", isBlog ? "Blog" : "Home", new { id = isBlog ? comment.PostId : comment.PropertyId });
        }
    }
}