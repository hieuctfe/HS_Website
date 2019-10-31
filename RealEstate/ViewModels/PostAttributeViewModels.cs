namespace RealEstate.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using RealEstate.Models.ComplexType;

    public class SharedPostCategoryViewModels
    {
        public int Id { get; set; }

        [DisplayName("Tên Danh Mục")]
        [Required, MaxLength(255)]
        public string Name { get; set; }
    }

    public class SharedPostLabelViewModels
    {
        public int Id { get; set; }

        [DisplayName("Tên Tags")]
        [Required, MaxLength(255)]
        public string Name { get; set; }
    }

    public class ManagePostAttributeViewModels
    {
        public IEnumerable<(int id, string name)> Item { get; set; }

        [Required, MaxLength(255)]
        public string ItemName { get; set; }

        public string ModalName { get; set; }
    }
}