namespace RealEstate.ViewModels.UI
{
    using System.ComponentModel;

    public class HomeUIOfferViewModels
    {
        [DisplayName("Tiêu Đề")]
        public string Title { get; set; }

        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Link Liên Kết")]
        public string LinkedUrl { get; set; }

        [DisplayName("Icon Đại Diện")]
        public string Icons { get; set; }
    }
}