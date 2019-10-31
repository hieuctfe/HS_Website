namespace RealEstate.Models.ComplexType
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class UIOption : _HelperContent
    {
        public UIOption()
        {
            IsDisplay = false;
        }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        [DisplayName("Hiển Thị")]
        public bool IsDisplay { get; set; }
    }
}