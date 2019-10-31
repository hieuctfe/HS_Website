namespace RealEstate.ViewModels
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using RealEstate.Models.ComplexType;

    public class PropertyPageViewModels
    {
        public PropertyPageViewModels()
        {
            PropertyPagination = new _PropertyPagination();
        }

        public _PropertyPagination PropertyPagination { get; set; }

        public IEnumerable<PropertyViewModels> Properties { get; set; }

        public IEnumerable<PropertyViewModels> Featured { get; set; }
    }

    public class _PropertyPagination : _HelperContent
    {
        public _PropertyPagination()
        {
            PageIndex = 1;
            PageSize = 8;

            AreaValue = "0;1000";
        }

        public bool IsSort => !string.IsNullOrEmpty(SortField);

        public bool IsSearch => !string.IsNullOrEmpty(Status)
                             || !string.IsNullOrEmpty(Type)
                             || !string.IsNullOrEmpty(Bath)
                             || !string.IsNullOrEmpty(Bed)
                             || !string.IsNullOrEmpty(City)
                             || !string.IsNullOrEmpty(District);

        public decimal? FromPriceValue { get; set; }

        public decimal? ToPriceValue { get; set; }

        public string AreaValue { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string City { get; set; }

        [DisplayName("Quận/Huyện")]
        public string District { get; set; }

        public string Bed { get; set; }

        public string Bath { get; set; }

        public string SortField { get; set; }

        public int PageIndex { get; set; }

        private int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize < 8 ? 8 : _pageSize;
            set => _pageSize = value;
        }
    }
}