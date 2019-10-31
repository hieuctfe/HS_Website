namespace RealEstate.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using RealEstate.Models.ComplexType;

    [Table("Posts")]
    public class Post
    {
        public Post()
        {
            ViewCount = 0;

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Bài Viết")]
        public string Name { get; set; }

        [Required]
        public string AvatarName { get; set; }

        [Required]
        public string AvatarPath { get; set; }

        public string HeaderImageName { get; set; }

        public string HeaderImagePath { get; set; }

        [MaxLength(1000)]
        [DisplayName("Tóm Tắt")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int ViewCount { get; set; }

        [ForeignKey("PostCategory")]
        public int PostCategoryId { get; set; }

        public ICollection<PostLabelData> PostLabelDatas { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public PostCategory PostCategory { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }
}