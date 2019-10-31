namespace RealEstate.ViewModels
{
    using RealEstate.Models.ComplexType;
    using System.Web.Routing;

    public class _Pager
    {
        public _Pager()
        {
            SearchValue = SortField = string.Empty;
            PageIndex = 1;
            PageSize = 20;
        }

        public string SearchValue { get; set; }

        public string SortField { get; set; }

        private int _pageIndex { get; set; }

        public int PageIndex { get; set; }

        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize < 20 ? 20 : _pageSize;
            set => _pageSize = value;
        }
    }

    public class _PageView : _HelperContent
    {
        public string Action { get; set; }

        public string Controller { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TotalPage { get; set; }

        public int PageIndex { get; set; }

        public int ShowPage { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }
}