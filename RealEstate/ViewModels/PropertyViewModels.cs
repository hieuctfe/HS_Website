namespace RealEstate.ViewModels
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using RealEstate.Models.ComplexType;

    public class PropertyDetailPageViewModels
    {
        public PropertyDetailPageViewModels()
        {
            PropertyPagination = new _PropertyPagination();
        }

        public _PropertyPagination PropertyPagination { get; set; }

        public PropertyDetailViewModels DetailInformation { get; set; }

        public IEnumerable<PropertyViewModels> Featured { get; set; }

        public IEnumerable<PropertyViewModels> Related { get; set; }
    }

    public class PropertyDetailViewModels
    {
        public PropertyDetailViewModels()
        {
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string DetailDescription { get; set; }

        public IEnumerable<(string name, string path)> HeaderImages { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public string ContactName { get; set; }

        public string ContactAddress { get; set; }

        public decimal? Price { get; set; }

        public string PriceFormated => Price.HasValue ? 
            Price.Value.ToString("#,#", CultureInfo.InvariantCulture) : "Liên hệ";

        public decimal Area { get; set; }

        public string AreaFormated => Area.ToString("#,#", CultureInfo.InvariantCulture);

        public int NumberOfBedRoom { get; set; }

        public int NumberOfBathRoom { get; set; }

        public int NumberOfGarage { get; set; }

        public bool HasSwimming { get; set; }

        public bool HasGarden { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public IEnumerable<CommentViewModels> Comments { get; set; }

        public CommentViewModels CreateComment { get; set; }

        public double Rating { get; set; }
    }

    public class PropertyViewModels : _Pager
    {
        public int Id { get; set; }

        [DisplayName("Mã")]
        public string Code { get; set; }

        [DisplayName("Tên Dự Án")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string AvatarName { get; set; }

        public string AvatarPath { get; set; }

        [DisplayName("Giá(VND)")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public string ContactPhone { get; set; }

        public string PriceFormated => Price.HasValue ? Price.Value != 0 ? Price.Value.ToString("#,#", CultureInfo.InvariantCulture) + " VND" : $"Liên hệ - {this.ContactPhone}" : $"Liên hệ - {this.ContactPhone}";

        [DisplayName("Diện Tích(m²)")]
        public decimal Area { get; set; }

        public string AreaFormated => Area.ToString("#,#", CultureInfo.InvariantCulture);

        [DisplayName("Phòng Ngủ")]
        public int NumberOfBedRoom { get; set; }

        [DisplayName("Phòng Tắm")]
        public int NumberOfBathRoom { get; set; }

        public int NumberOfGarage { get; set; }

        [DisplayName("Nhà Xe")]
        public bool HasGarage { get; set; }

        [DisplayName("Quận/Huyện")]
        public string District { get; set; }

        [DisplayName("Thành Phố")]
        public string City { get; set; }

        [DisplayName("Loại Giao Dịch")]
        public string Status { get; set; }

        [DisplayName("Loại")]
        public string Type { get; set; }

        [DisplayName("Trạng Thái")]
        public string OrderStatus { get; set; }

        [DisplayName("Hiển Thị")]
        public bool IsDisplay { get; set; }

        [DisplayName("Nổi Bật")]
        public bool IsFeatured { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }

    public class PropertyBasicInformation : _HelperContent
    {
        public PropertyBasicInformation()
        {
            NumberOfBedRoom = 0;
        }

        public int Id { get; set; }

        [DisplayName("Mã Dự Án")]
        [MaxLength(20, ErrorMessage = "{0} Không Được Quá {1} Kí Tự")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} Không Được Để Trống"),
            MaxLength(255, ErrorMessage = "Tên Dự Án Không Được Quá {1} Kí Tự")]
        [DisplayName("Tên Dự Án")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Dự Án Không Được Để Trống")]
        [DisplayName("Giá")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [DisplayName("Diện Tích")]
        public decimal Area { get; set; }

        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [DisplayName("Phòng Ngủ")]
        public int NumberOfBedRoom { get; set; }

        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [DisplayName("Phòng Tắm")]
        public int NumberOfBathRoom { get; set; }

        [Required]
        public int NumberOfGarage { get; set; }

        [Required]
        [DisplayName("Bể Bơi")]
        public bool HasSwimming { get; set; }

        [Required]
        [DisplayName("Vườn")]
        public bool HasGarden { get; set; }

        [Required]
        [DisplayName("Nhà Để Xe")]
        public bool HasCarGarage { get; set; }

        [MaxLength(255, ErrorMessage = "{0} Không Được Quá 225 Kí Tự")]
        [DisplayName("Tiêu Đề Cho Bài Viết")]
        public string PostTitle { get; set; }

        [MaxLength(255, ErrorMessage = "{0} Không Được Quá 225 Kí Tự")]
        [DisplayName("Tóm Tắt Về Sản Phẩm")]
        public string ShortDescription { get; set; }

        [DisplayName("Bài Viết Cho Sản Phẩm"), AllowHtml]
        public string DetailDescription { get; set; }

        [MaxLength(510, ErrorMessage = "{0} Không Được Quá 510 Kí Tự")]
        [DisplayName("Địa Chỉ")]
        public string AddressLine { get; set; }

        [MaxLength(255, ErrorMessage = "{0} Không Được Quá 255 Kí Tự")]
        [DisplayName("Quận/Huyện")]
        public string District { get; set; }

        [MaxLength(255, ErrorMessage = "{0} Không Được Quá 255 Kí Tự")]
        [DisplayName("Thành Phố")]
        public string City { get; set; }

        [DisplayName("Nhu Cầu Dự Án")]
        public int PropertyStatusCodeId { get; set; }

        [DisplayName("Loại Dự Án")]
        public int PropertyTypeId { get; set; }

        [DisplayName("Nổi Bật")]
        public bool IsFeatured { get; set; }

        [DisplayName("Hiển Thị")]
        public bool IsDisplay { get; set; }
    }

    public class PropertyContacInformation : _HelperContent
    {
        [MaxLength(255, ErrorMessage = "{0} Không Được Quá {1} Kí Tự")]
        [DisplayName("Tên Liên Hệ")]
        public string ContactName { get; set; }

        [MaxLength(510, ErrorMessage = "{0} Không Được Quá {1} Kí Tự")]
        [DisplayName("Địa Chỉ")]
        public string ContactAddress { get; set; }

        [MaxLength(20, ErrorMessage = "{0} Không Được Quá {1} Kí Tự")]
        [Phone(ErrorMessage = "{0} Không Hợp Lệ")]
        [DisplayName("Số Điện Thoại")]
        public string ContactPhoneNumber { get; set; }

        [MaxLength(255, ErrorMessage = "{0} Không Được Quá {1} Kí Tự")]
        [EmailAddress(ErrorMessage = "{0} Không Hợp Lệ")]
        [DisplayName("Địa Chỉ Email")]
        public string ContactEmail { get; set; }
    }

    public class CreatePropertyViewModels
    {
        public CreatePropertyViewModels()
        {
            BasicInformation = new PropertyBasicInformation();
            ContactInformation = new PropertyContacInformation();

            Avatar = new List<_ImageCropper>();
            PropertyImages = new List<_ImageCropper>();

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        public PropertyBasicInformation BasicInformation { get; set; }

        public PropertyContacInformation ContactInformation { get; set; }

        public string AvatarUpload
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(Avatar) : string.Empty;

        public string ImageUploaded
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(PropertyImages) : string.Empty;

        public ICollection<_ImageCropper> Avatar { get; set; }

        public ICollection<_ImageCropper> PropertyImages { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }

    public class UpdatePropertyViewModels : _HelperContent
    {
        public UpdatePropertyViewModels()
        {
            BasicInformation = new PropertyBasicInformation();
            ContactInformation = new PropertyContacInformation();

            Avatar = new List<_ImageCropper>();
            PropertyImages = new List<_ImageCropper>();

            Seo = new Seo();
            UIOption = new UIOption();
            ActivityLog = new ActivityLog();
        }

        public PropertyBasicInformation BasicInformation { get; set; }

        public PropertyContacInformation ContactInformation { get; set; }

        public string AvatarUpload
            => Avatar.Count > 0 ? JsonConvert.SerializeObject(Avatar) : string.Empty;

        public string ImageUploaded
            => PropertyImages.Count > 0 ? JsonConvert.SerializeObject(PropertyImages) : string.Empty;

        public ICollection<_ImageCropper> Avatar { get; set; }

        public ICollection<_ImageCropper> PropertyImages { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public UIOption UIOption { get; set; }

        public Seo Seo { get; set; }
    }
}