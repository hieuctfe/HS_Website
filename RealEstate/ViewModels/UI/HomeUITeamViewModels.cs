namespace RealEstate.ViewModels.UI
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class HomeUITeamViewModels : _ImageCropper
    {
        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Ảnh Đại Diện")]
        public string AvatarUploaded
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