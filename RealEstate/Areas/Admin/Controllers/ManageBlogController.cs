namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.ViewModels;
    using RealEstate.Infrastructure;
    using RealEstate.Areas.Admin.Controllers.Shared;

    public class ManageBlogController : BaseAdminController
    {
        #region Index
        [HttpGet]
        public ActionResult Index(_Pager pager)
        {
            IQueryable<Post> posts = _context.Posts.Include(x => x.Comments);

            int pageIndex = pager.PageIndex,
                pageSize = pager.PageSize;

            string searchValue = pager.SearchValue,
                sortFieldValue = $"{nameof(Property.Name)}";//default order field
            string orderByValue = "asc";//default order by

            if (!string.IsNullOrEmpty(pager.SortField))
            {
                string[] segments = pager.SortField.Split('_');

                sortFieldValue = segments[0];
                orderByValue = segments[1] == "asc" ? "asc" : "desc";
            }//end if check is sort field has value

            posts = posts.Where(x => string.IsNullOrEmpty(searchValue) || x.Name.Contains(searchValue));

            int totalPages = (int)Math.Ceiling(posts.Count() / (double)pageSize);


            ViewBag.SortField = pager.SortField;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchValue = searchValue;

            return View(_converter.CreatePostViewModels(posts.OrderByPropertyName(sortFieldValue, orderByValue)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList()));
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = GetCategories(-1);
            ViewBag.Labels = GetLabels(null);

            return View(new CreatePostViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModels post)
        {
            ViewBag.Categories = GetCategories(-1);
            ViewBag.Labels = GetLabels(null);

            if (ModelState.IsValid)
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    _ImageCropper img;
                    Post newPost = _converter.CreatePost(post);

                    img = SaveImage(post.Avatar.FirstOrDefault());
                    newPost.AvatarName = img.ImageName;
                    newPost.AvatarPath = img.ImagePath;

                    img = SaveImage(post.HeaderImage.FirstOrDefault());
                    newPost.HeaderImageName = img.ImageName;
                    newPost.HeaderImagePath = img.ImagePath;

                    _context.Entry(newPost).State = EntityState.Added;
                    _context.SaveChanges();

                    transaction.Commit();
                }//end transaction

                return RedirectToAction(nameof(Index));
            }//end if handle case model is not valid

            return View(post);
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Post post = _context.Posts.Include(x => x.PostCategory)
                                          .Include(x => x.PostLabelDatas).FirstOrDefault(x => x.Id == id.Value);

                if (post != null)
                {
                    ViewBag.Categories = GetCategories(post.PostCategoryId);
                    ViewBag.Labels = GetLabels(post.PostLabelDatas.Select(x => x.PostLabelId));

                    return View(_converter.CreateUpdatePostViewModels(post));
                } else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }//end if handle case property is not existed
            }//end if handle case args is null

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? id, UpdatePostViewModels post)
        {
            if (id.HasValue && ModelState.IsValid)
            {
                Post basePost = _context.Posts.Include(x => x.PostLabelDatas)
                                            .FirstOrDefault(x => x.Id == id.Value);

                if (basePost != null)
                {
                    using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                    {
                        _ImageCropper img;
                        _converter.UpdatePost(post, basePost);

                        img = post.Avatar.FirstOrDefault();
                        SaveImage(img);
                        basePost.AvatarName = img.ImageName;
                        basePost.AvatarPath = img.ImagePath;

                        img = post.HeaderImage.FirstOrDefault();
                        SaveImage(img);
                        basePost.HeaderImageName = img.ImageName;
                        basePost.HeaderImagePath = img.ImagePath;

                        List<PostLabelData> baseLabels = basePost.PostLabelDatas.ToList();
                        baseLabels.ForEach(x => _context.Entry(x).State = EntityState.Deleted);

                        post.BasicInformation.PostLabelIds.ToList().ForEach(x =>
                        {
                            PostLabelData label = new PostLabelData { PostId = basePost.Id, PostLabelId = x };

                            if (baseLabels.Contains(label))
                            {
                                int index = baseLabels.IndexOf(label);
                                _context.Entry(baseLabels[index]).State = EntityState.Unchanged;
                            }
                            else
                            {
                                _context.Entry(label).State = EntityState.Added;
                            }//end if check is label existed in database
                        });//end for update label datas

                        _context.SaveChanges();
                        transaction.Commit();
                    }//end transaction

                    return RedirectToAction(nameof(Index));
                }//end if handle case model is not valid
            }//end if handle case args is null

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        #endregion

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
                        ids.ToList().ForEach(id => _context.Entry(new Post { Id = id }).State = EntityState.Deleted);

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
                    Post post = _context.Posts.FirstOrDefault(x => x.Id == id.Value);

                    if (post != null)
                    {
                        using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                        {
                            post.UIOption.IsDisplay = !post.UIOption.IsDisplay;

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