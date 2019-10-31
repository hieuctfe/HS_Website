namespace RealEstate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModels
    {
        public CommentViewModels()
        {
            CreatedOn = DateTimeOffset.Now;
        }

        public int Id { get; set; }

        public int? PostId { get; set; }

        public int? PropertyId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên")]
        public string Owner { get; set; }
        
        [Required(ErrorMessage = "Vui lòng điền nội dung")]
        public string Description { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int? ParentId { get; set; }

        public CommentViewModels Parent { get; set; }

        public ICollection<CommentViewModels> Child { get; set; }
    }

    public class ReadCommentViewModels : _Pager
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string Content { get; set; }

        public bool IsVerify { get; set; }

        public int? Rating { get; set; }

        public string UrlLinked { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}