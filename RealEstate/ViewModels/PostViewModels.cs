namespace RealEstate.ViewModels
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using RealEstate.Models.ComplexType;
    using RealEstate.Infrastructure;

    public class PostDetailViewModels
    {
        public int Id { get; set; }

        [DisplayName("Bài Viết")]
        public string Name { get; set; }

        public string HeaderImageName { get; set; }

        public string HeaderImagePath { get; set; }

        public int CommentCount => Comments.Safe().Count();

        public string Description { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public IEnumerable<SharedPostLabelViewModels> Labels { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public CommentViewModels CreateComment { get; set; }

        public IEnumerable<CommentViewModels> Comments { get; set; }
    }

    public class PostViewModels : _Pager
    {
        public PostViewModels()
        {
            CommentCount = 0;
        }

        public int Id { get; set; }

        [DisplayName("Bài Viết")]
        public string Name { get; set; }

        [DisplayName("Hiển Thị")]
        public bool IsDisplay { get; set; }

        public string AvatarName { get; set; }

        public string AvatarPath { get; set; }

        public int CommentCount { get; set; }

        public int ViewCount { get; set; }

        [DisplayName("Tóm Tắt")]
        public string Description { get; set; }

        [DisplayName("Người Đăng")]
        public string CreatedBy { get; set; }

        [DisplayName("Ngày Đăng")]
        public DateTimeOffset CreatedOn { get; set; }
    }

    public class PostBasicInformation : _HelperContent
    {
        public PostBasicInformation()
        {
            PostLabelIds = Enumerable.Empty<int>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu Đề Bài Viết Không Được Để Trống"), 
            MaxLength(255, ErrorMessage = "Tiêu Đề Bài Viết Không Được Quá 225 Kí Tự")]
        [DisplayName("Tiêu Đề Bài Viết")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Tóm Tắt Không Được Quá 1000 Kí Tự")]
        [DisplayName("Tóm Tắt")]
        public string Description { get; set; }

        [AllowHtml]
        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Hiển Thị")]
        public bool IsDisplay { get; set; }

        [DisplayName("Danh Mục")]
        public int PostCategoryId { get; set; }

        [DisplayName("Tags")]
        public IEnumerable<int> PostLabelIds { get; set; }
    }

    public class CreatePostViewModels
    {
        public CreatePostViewModels()
        {
            BasicInformation = new PostBasicInformation();

            Avatar = new List<_ImageCropper>();
            HeaderImage = new List<_ImageCropper>();

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        public PostBasicInformation BasicInformation { get; set; }

        public string AvatarUpload
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(Avatar) : string.Empty;

        public string ImageUpload
            => HeaderImage.Count > 0 ? JsonConvert.SerializeObject(HeaderImage) : string.Empty;

        public ICollection<_ImageCropper> Avatar { get; set; }

        public ICollection<_ImageCropper> HeaderImage { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }

    public class UpdatePostViewModels
    {
        public UpdatePostViewModels()
        {
            BasicInformation = new PostBasicInformation();

            Avatar = new List<_ImageCropper>();
            HeaderImage = new List<_ImageCropper>();

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        public PostBasicInformation BasicInformation { get; set; }

        public string AvatarUpload
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(Avatar) : string.Empty;

        public string ImageUpload
            => HeaderImage.Count > 0 ? JsonConvert.SerializeObject(HeaderImage) : string.Empty;

        public ICollection<_ImageCropper> Avatar { get; set; }

        public ICollection<_ImageCropper> HeaderImage { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }
}