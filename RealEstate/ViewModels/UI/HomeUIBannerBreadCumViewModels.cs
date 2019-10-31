namespace RealEstate.ViewModels.UI
{
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Collections.Generic;

    public class HomeUIBannerBreadCumViewModels : _ImageCropper
    {
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