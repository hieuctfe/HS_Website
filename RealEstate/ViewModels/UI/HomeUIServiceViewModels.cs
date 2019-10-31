namespace RealEstate.ViewModels.UI
{
    using System.ComponentModel;
    using System.Collections.Generic;

    public class HomeUIServiceViewModels
    {
        public HomeUIServiceViewModels()
        {
            Services = new List<ServiceViewModels>();
        }

        public ICollection<ServiceViewModels> Services { get; set; }

        [DisplayName("Sologan")]
        public string Sologan { get; set; }

        [DisplayName("Tác Giả")]
        public string Author { get; set; }
    }

    public class ServiceViewModels
    {
        [DisplayName("Tiêu Đề")]
        public string Title { get; set; }

        [DisplayName("Nội Dung")]
        public string Content { get; set; }

        [DisplayName("Link Liên Kết")]
        public string LinkedUrl { get; set; }

        [DisplayName("Icon Đại Diện")]
        public string Icons { get; set; }

        [DisplayName("Delay")]
        public int Delay { get; set; }
    }
}