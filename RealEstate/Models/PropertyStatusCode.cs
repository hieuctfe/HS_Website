namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RealEstate.Models.ComplexType;

    [Table("PropertyStatusCodes")]
    public class PropertyStatusCode
    {
        public PropertyStatusCode()
        {
            UIOption = new UIOption();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Loại Giao Dịch")]
        public string Name { get; set; }

        [MaxLength(510)]
        public string Description { get; set; }

        public UIOption UIOption { get; set; }

        public ICollection<Property> Properties { get; set; }

    }
}