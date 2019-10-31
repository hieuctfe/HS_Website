namespace RealEstate.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using RealEstate.EnumType.Models;
    using RealEstate.Models.ComplexType;

    [Table("Properties")]
    public class Property
    {
        public Property()
        {
            HasCarGarage = HasGarden = HasSwimming = false;
            NumberOfBathRoom = NumberOfBedRoom = NumberOfGarage = 0;
            OrderStatus = OrderStatus.InProcess;

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [DisplayName("Mã")]
        public string Code { get; set; }

        [Required, MaxLength(255)]
        [DisplayName("Tên Dự Án")]
        public string Name { get; set; }

        public string AvatarName { get; set; }

        public string AvatarPath { get; set; }

        [MaxLength(255)]
        public string ContactName { get; set; }

        [MaxLength(510)]
        public string ContactAddress { get; set; }

        [Phone]
        public string ContactPhoneNumber { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [DisplayName("Giá(VND)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Diện Tích(m²)")]
        public decimal Area { get; set; }

        [Required]
        [DisplayName("Phòng Ngủ")]
        public int NumberOfBedRoom { get; set; }

        [Required]
        [DisplayName("Phòng Tắm")]
        public int NumberOfBathRoom { get; set; }

        [Required]
        [DisplayName("Nhà Xe")]
        public int NumberOfGarage { get; set; }

        [Required]
        public bool HasSwimming { get; set; }

        [Required]
        [DisplayName("Nổi Bật")]
        public bool IsFeatured { get; set; }

        [Required]
        public bool HasGarden { get; set; }

        [Required]
        [DisplayName("Nhà Xe")]
        public bool HasCarGarage { get; set; }

        [MaxLength(255)]
        [Column("Title")]
        public string PostTitle { get; set; }

        [MaxLength(255)]
        public string ShortDescription { get; set; }

        [Column(TypeName = "ntext")]
        public string DetailDescription { get; set; }

        [MaxLength(510)]
        public string AddressLine { get; set; }

        [MaxLength(255)]
        [DisplayName("Quận/Huyện")]
        public string District { get; set; }

        [MaxLength(255)]
        [DisplayName("Thành Phố")]
        public string City { get; set; }

        [DisplayName("Trạng Thái")]
        public OrderStatus OrderStatus { get; set; }

        [ForeignKey("PropertyStatusCode")]
        public int PropertyStatusCodeId { get; set; }

        [ForeignKey("PropertyType")]
        public int PropertyTypeId { get; set; }

        public PropertyStatusCode PropertyStatusCode { get; set; }

        public PropertyType PropertyType { get; set; }

        public ICollection<PropertyImage> PropertyImages { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }
}