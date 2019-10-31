namespace RealEstate.ViewModels.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Newtonsoft.Json;

    public class HomeUIBannerViewModels : _ImageCropper
    {
        [DisplayName("Tiêu Đề")]
        public string Title { get; set; }

        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Link Liên Kết")]
        public string LinkedUrl { get; set; }

        [DisplayName("Link Mở Rộng")]
        public string ExtraUrl { get; set; }

        [DisplayName("Ảnh Nền")]
        public string BackgroundUploaded
            => !string.IsNullOrEmpty(ImageName) ? JsonConvert.SerializeObject(new List<_ImageCropper>()
            {
                new _ImageCropper
                {
                    ImageData = ImageData,
                    ImagePath = ImagePath,
                    ImageName = ImageName,
                    IsUploaded = IsUploaded
                }
            }) : string.Empty;
    }
}