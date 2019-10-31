namespace RealEstate.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Models;

    public class PropertyTypeGeneration : DataGeneration
    {
        public PropertyTypeGeneration(_Context context) : base(context)
        {
            Generate();
        }

        #region static data
        private IEnumerable<string> _propertyType => new List<string>
        {
            "Nhà Ở", "Văn Phòng", "Chung Cư", "Biệt Thự", "Phòng Đơn", "Phòng Đôi"
        };
        #endregion

        public override void Generate()
        {
            _propertyType.ToList().ForEach(x => _context.PropertyTypes.AddOrUpdate(p => p.Name, new PropertyType { Name = x }));
            _context.SaveChanges();
        }
    }
}