namespace RealEstate.ViewModels
{
    using System.Collections.Generic;
    using RealEstate.Models.ComplexType;

    public class PostPageViewModels
    {
        public IEnumerable<SharedPostCategoryViewModels> Categories { get; set; }

        public IEnumerable<SharedPostLabelViewModels> Labels { get; set; }

        public IEnumerable<PostViewModels> Posts { get; set; }

        public IEnumerable<PostViewModels> TopCommentPosts { get; set; }

        public IEnumerable<PostViewModels> TopViewPosts { get; set; }

        public IEnumerable<PostViewModels> RecentPosts { get; set; }

        public _PostPagination PostPagination { get; set; }
    }
    
    public class _PostPagination : _HelperContent
    {
        public _PostPagination()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public string SearchValue { get; set; }

        public string SortField { get; set; }

        public int PageIndex { get; set; }

        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize < 10 ? 10 : _pageSize;
            set => _pageSize = value;
        }
    }
}