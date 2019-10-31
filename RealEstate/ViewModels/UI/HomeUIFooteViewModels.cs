using System.ComponentModel;
using System.Web.Mvc;

namespace RealEstate.ViewModels.UI
{

    public class HomeUIFooteViewModels
    {
        [AllowHtml]
        [DisplayName("Footer HTML")]
        public string FooterHtml { get; set; }
    }
}