namespace RealEstate.ViewModels.UI
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class HomeUIAchivementViewModels
    {
        public HomeUIAchivementViewModels()
        {
            Achivements = new List<AchivementViewModels>();
            Background = new List<_ImageCropper>();
        }

        public ICollection<AchivementViewModels> Achivements { get; set; }

        public ICollection<_ImageCropper> Background { get; set; }

        [DisplayName("Background")]
        public string BackgroundUploaded
            => Background.Count > 0 ? JsonConvert.SerializeObject(Background) : string.Empty;
    }

    public class AchivementViewModels
    {
        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Số Lượng")]
        public int StopAt { get; set; }

        [DisplayName("Tốc Độ")]
        public int Speed { get; set; }

        [DisplayName("Delay")]
        public int Delay { get; set; }
    }
}