namespace RealEstate.ViewModels
{
    using Newtonsoft.Json;

    public partial class _ImageCropper
    {
        public _ImageCropper() { }

        public _ImageCropper(string imageName, string imagePath)
        {
            ImageName = imageName;
            ImagePath = imagePath;
        }

        [JsonIgnore]
        public string ButtonText { get; set; }

        [JsonIgnore]
        public string ImageUploaded { get; set; }

        [JsonIgnore]
        public string CropActiveId { get; set; }

        [JsonIgnore]
        public string ImageContainer { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }

        public string ImageData { get; set; }

        public bool IsUploaded { get; set; }
    }
}