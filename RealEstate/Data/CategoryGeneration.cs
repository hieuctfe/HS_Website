namespace RealEstate.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Models;

    public class CategoryGeneration : DataGeneration
    {
        public CategoryGeneration(_Context context) : base(context)
        {
            Generate();
        }

        #region static data
        private string[] _randomName = { "Bất Động Sản", "Thiết Kế Không Gian", "Phong Thủy", "Nội Thất",
                                         "Đánh Giá Dự Án", "Luật Nhà Đất", "Chia Sẻ Kinh Nghiệm", "Tin Tức Nổi Bật" };
        #endregion

        public override void Generate()
        {
            _randomName.ToList().ForEach(x =>
            {
                _context.PostCategories.AddOrUpdate(y => y.Name, new PostCategory
                {
                    Name = x
                });
            });
            _context.SaveChanges();
        }
    }
}