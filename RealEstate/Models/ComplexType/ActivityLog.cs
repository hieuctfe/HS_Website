namespace RealEstate.Models.ComplexType
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class ActivityLog : _HelperContent
    {
        public ActivityLog()
        {
            CreatedOn = ModifiedOn = DateTimeOffset.Now;
            CreatedBy = ModifiedBy = "Unknown";
        }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public DateTimeOffset ModifiedOn { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Người Đăng")]
        public string CreatedBy { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Ngày Đăng")]
        public string ModifiedBy { get; set; }
    }
}