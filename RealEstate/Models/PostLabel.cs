namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RealEstate.Models.ComplexType;

    [Table("PostLabels")]
    public class PostLabel
    {
        public PostLabel()
        {
            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public ICollection<PostLabelData> PostLabelDatas { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }
}