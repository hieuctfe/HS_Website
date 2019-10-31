namespace RealEstate.Models.UI
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UICaches")]
    public class UICache
    {
        public UICache(string pageName, string componentName, string dataType)
        {
            PageName = pageName;
            ComponentName = componentName;
            DataType = dataType;
        }

        public UICache() { }

        [Required]
        [MaxLength(255)]
        [Key, Column(Order = 1)]
        public string PageName { get; set; }

        [Required]
        [MaxLength(255)]
        [Key, Column(Order = 2)]
        public string ComponentName { get; set; }

        [Required]
        [MaxLength(20)]
        [Key, Column(Order = 3)]
        public string DataType { get; set; }

        [Column(TypeName = "ntext")]
        public string DataCache { get; set; }
    }
}