namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        public Comment()
        {
            IsVerify = false;
            CreatedOn = DateTimeOffset.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Khách Hàng")]
        public string Owner { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required, MaxLength(1000)]
        [DisplayName("Nội Dung")]
        public string Description { get; set; }

        [Required, Range(1, 5)]
        [DisplayName("Rate")]
        public int Rating { get; set; }

        [Required]
        public bool IsVerify { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTimeOffset CreatedOn { get; set; }

        public int? PostId { get; set; }

        [DisplayName("Link Liên Kết")]
        public int? PropertyId { get; set; }

        public int? ParentId { get; set; }
        
        [ForeignKey("ParentId")]
        public Comment Parent { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

        public ICollection<Comment> Childrens { get; set; }
    }
}