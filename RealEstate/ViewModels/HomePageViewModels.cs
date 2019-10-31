namespace RealEstate.ViewModels
{
    using System.Collections.Generic;

    public class HomePageViewModels
    {
        public string BannerHtml { get; set; }

        public string OfferHtml { get; set; }

        public string ServiceHtml { get; set; }

        public string AchivementHtml { get; set; }

        public string TeamHtml { get; set; }

        public string ClientHtml { get; set; }

        public ICollection<PropertyViewModels> RecentProperties { get; set; }

        public ICollection<PropertyViewModels> FeaturedProperties { get; set; }

        public ICollection<PostViewModels> RecentPosts { get; set; }
    }
}