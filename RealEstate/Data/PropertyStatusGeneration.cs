namespace RealEstate.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Models;

    public class PropertyStatusGeneration : DataGeneration
    {
        public PropertyStatusGeneration(_Context context) : base(context)
        {
            Generate();
        }

        #region static data
        private IEnumerable<string> _propertyStatus => new List<string>
        {
            "Cho Thuê", "Bán"
        };
        #endregion

        public override void Generate()
        {
            _propertyStatus.ToList().ForEach(x => _context.PropertyStatusCodes.AddOrUpdate(p => p.Name, new PropertyStatusCode { Name = x }));
            _context.SaveChanges();
        }
    }
}