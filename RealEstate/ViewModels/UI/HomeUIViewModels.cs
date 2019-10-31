namespace RealEstate.ViewModels.UI
{
    using System.Collections.Generic;

    public class HomeUIViewModels
    {
        public HomeUIViewModels()
        {
            Slider = new List<HomeUIBannerViewModels>();
        }

        public ICollection<HomeUIBannerViewModels> Slider { get; set; }
    }
}