namespace RealEstate.Models.ComplexType
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class Seo : _HelperContent
    {
        [MaxLength(255, ErrorMessage = "Tiêu Đề Hiển Thị Không Được Quá 255 Kí Tự")]
        [DisplayName("Tiêu Đề Hiển Thị")]
        public string Title { get; set; }

        [DisplayName("Meta-Content")]
        public string MetaContent { get; set; }

        [DisplayName("Meta-Description")]
        public string MetaDescription { get; set; }

        [DisplayName("Structure-Data")]
        public string StructureData { get; set; }

        [DisplayName("Tùy Chỉnh Url")]
        public string FriendlyUrl { get; set; }

        [DisplayName("Tên Hiển Thị")]
        public string AliasName { get; set; }
    }
}