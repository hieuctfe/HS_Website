namespace RealEstate.ViewModels
{
    using System.Collections.Generic;

    public class PropertyMenuViewModels
    {
        public IEnumerable<UpdateMenuItem> PropertyStatusCode { get; set; }

        public IEnumerable<UpdateMenuItem> PropertyTypes { get; set; }

        public IEnumerable<UpdateMenuItem> BlogProperty { get; set; }
    }
}