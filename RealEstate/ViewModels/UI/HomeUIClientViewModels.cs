namespace RealEstate.ViewModels.UI
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class HomeUIClientViewModels
    {
        public HomeUIClientViewModels()
        {
            Clients = new List<ClientViewModels>();
            Background = new List<_ImageCropper>();
        }

        public ICollection<ClientViewModels> Clients { get; set; }

        public ICollection<_ImageCropper> Background { get; set; }

        [DisplayName("Background")]
        public string BackgroundUploaded
            => Background.Count > 0 ? JsonConvert.SerializeObject(Background) : string.Empty;
    }

    public class ClientViewModels : _ImageCropper
    {
        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Tên")]
        public string Name { get; set; }

        [DisplayName("Chức Vụ/Vị Trí")]
        public string Position { get; set; }

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