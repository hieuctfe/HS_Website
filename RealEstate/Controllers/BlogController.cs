namespace RealEstate.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Controllers.Shared;
    using RealEstate.Infrastructure;

    public class BlogController : BaseController
    {
        #region Index
        [HttpGet]
        public ActionResult Index(int? categoryId, int? labelId, _PostPagination pager)
        {
            ViewBag.BannerImage = base.BreadCumImage;

            IQueryable<Post> posts = _context.Posts.Include(x => x.Comments)
                .Where(x => x.UIOption.IsDisplay && 
                            categoryId.HasValue ? x.PostCategoryId == categoryId : true &&
                            labelId.HasValue ? x.PostLabelDatas.Any(y => y.PostLabelId == labelId) : true);

            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string searchValue = pager.SearchValue,
                sortFieldValue = $"{nameof(Property.UIOption)}." +
                                 $"DisplayOrder";//default order field;
            string orderByValue = "asc";//default order by

            if (!string.IsNullOrEmpty(pager.SortField))
            {
                string[] segments = pager.SortField.Split('_');

                sortFieldValue = segments[0];
                orderByValue = segments[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            var recentPost = posts.OrderByDescending(x => x.ActivityLog.CreatedOn).Take(4).ToList();
            var topViewPost = posts.OrderByDescending(x => x.ViewCount).Take(4).ToList();
            var topCommentPost = posts.OrderByDescending(x => x.Comments.Count).Take(4).ToList();

            posts = posts.Where(x => string.IsNullOrEmpty(searchValue) || x.Name.Contains(searchValue));
            int totalPages = (int)Math.Ceiling(posts.Count() / (double)pageSize);

            ViewBag.SortField = pager.SortField;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchValue = searchValue;

            return View(new PostPageViewModels
            {
                Posts = _converter.CreatePostViewModels(posts.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList()),

                RecentPosts = _converter.CreatePostViewModels(recentPost),
                TopViewPosts = _converter.CreatePostViewModels(topViewPost),
                TopCommentPosts = _converter.CreatePostViewModels(topCommentPost),

                Categories = _converter.CreatePostCategories(_context.PostCategories),
                Labels = _converter.CreatePostLabels(_context.PostLabels),

                PostPagination = pager
            });
        }
        #endregion

        #region Detail
        [HttpGet]
        public ActionResult Detail(int? id, string title)
        {
            if (id.HasValue)
            {
                ViewBag.BannerImage = base.BreadCumImage;
                IQueryable<Post> posts = _context.Posts.Include(x => x.Comments).Where(x => x.UIOption.IsDisplay);

                Post post = _context.Posts.Include(x => x.Comments)
                                          .Include(x => x.PostLabelDatas.Select(y => y.PostLabel))
                                          .FirstOrDefault(x => x.Id == id.Value);

                var recentPost = posts.OrderByDescending(x => x.ActivityLog.CreatedOn).Take(4).ToList();
                var topViewPost = posts.OrderByDescending(x => x.ViewCount).Take(4).ToList();
                var topCommentPost = posts.OrderByDescending(x => x.Comments.Count).Take(4).ToList();

                ViewBag.Labels = _converter.CreatePostLabels(_context.PostLabels);
                ViewBag.Categories = _converter.CreatePostCategories(_context.PostCategories);
                ViewBag.RecentPosts = _converter.CreatePostViewModels(recentPost);
                ViewBag.TopViewPosts = _converter.CreatePostViewModels(topViewPost);
                ViewBag.TopCommentPosts = _converter.CreatePostViewModels(topCommentPost);

                string friendlyUrl = post.Name.RewriteUrl(maxLength: 5000);
                if (!string.Equals(friendlyUrl, title, StringComparison.Ordinal))
                {
                    return this.RedirectToRoutePermanent("BlogDetails", new { id = post.Id, title = friendlyUrl });
                }

                if (post != null)
                    return View(_converter.CreatePostDetailViewModels(post));
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        #endregion
    }
}