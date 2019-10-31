namespace RealEstate.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Models;

    public class LabelGeneration : DataGeneration
    {
        public LabelGeneration(_Context context) : base(context)
        {
            Generate();
        }

        #region static data
        private string[] _randomName = { "tiền tỷ", "xe máy", "cho vay", "cho thuê", "sách hay",
                                        "nội thất", "phụ kiện", "hiện đại", "cổ điển", "giá rẻ" };
        #endregion

        public override void Generate()
        {
            _randomName.ToList().ForEach(x =>
            {
                _context.PostLabels.AddOrUpdate(y => y.Name, new PostLabel
                {
                    Name = x
                });
            });
            _context.SaveChanges();
        }
    }
}