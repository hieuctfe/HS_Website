namespace RealEstate.Models
{
    using RealEstate.Models.ComplexType;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PostCategories")]
    public class PostCategory
    {
        public PostCategory()
        {
            UIOption = new UIOption();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public UIOption UIOption { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}