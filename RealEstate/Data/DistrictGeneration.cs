namespace RealEstate.Data
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using RealEstate.Models;

    public class DistrictGeneration : DataGeneration
    {
        public DistrictGeneration(_Context context) : base(context)
        {
            Generate();
        }

        #region static data
        private List<List<string>> _districts = new List<List<string>>
        {
            new List<string> { "An Phú", "Châu Đốc", "Châu Phú", "Châu Thành", "Chợ Mới", "Long Xuyên", "Phú Tân", "Tân Châu", "Thoại Sơn", "Tịnh Biên", "Tri Tôn" },
            new List<string> { "Bà Rịa", "Côn Đảo", "Đất Đỏ", "Long Điền", "Phú Mỹ", "Vũng Tàu", "Xuyên Mộc" },
            new List<string> { "Bắc Giang", "Hiệp Hòa", "Lạng Giang", "Lục Nam", "Lục Ngạn", "Sơn Động", "Tân Yên", "Việt Yên", "Yên Dũng", "Yên Thế" },
            new List<string> { "Ba Bể", "Bắc Kạn", "Bạch Thông", "Chợ Đồn", "Chợ Mới", "Na Rì", "Pác Nặm" },
            new List<string> { "Bạc Liêu", "Đông Hải", "Giá Rai", "Hòa Bình", "Hồng Dân", "Phước Long", "Vĩnh Lợi" },
            new List<string> { "Bắc Ninh", "Gia Bình", "Lương Tài", "Quế Võ", "Thuận Thành", "Tiên Du", "Từ Sơn", "Yên Phong" },
            new List<string> { "Ba Tri", "Bến Tre", "Bình Đại", "Châu Thành", "Chợ Lách", "Giồng Trôm", "Mỏ Cày Bắc", "Mỏ Cày Nam", "Thạnh Phú" },
            new List<string> { "An Lão", "An Nhơn", "Hoài Ân", "Hoài Nhơn", "Phù Cát", "Phù Mỹ", "Quy Nhơn", "Tây Sơn", "Tuy Phước", "Vân Canh", "Vĩnh Thạnh" },
            new List<string> { "Bắc Tân Uyên", "Bàu Bàng", "Bến Cát", "Dầu Tiếng", "Dĩ An", "Phú Giáo", "Tân Uyên", "Thủ Dầu Một", "Thuận An" },
            new List<string> { "Bình Long", "Bù Đăng", "Bù Đốp", "Bù Gia Mập", "Chơn Thành", "Đồng Phú", "Đồng Xoài", "Hớn Quản", "Lộc Ninh", "Phú Riềng", "Phước Long" },
            new List<string> { "Bắc Bình", "Đức Linh", "Hàm Tân", "Hàm Thuận Bắc", "Hàm Thuận Nam", "La Gi", "Phan Thiết", "Phú Quý", "Tánh Linh", "Tuy Phong" },
            new List<string> { "Cà Mau", "Cái Nước", "Đầm Dơi", "Năm Căn", "Ngọc Hiển", "Phú Tân", "Thới Bình", "Trần Văn Thời", "U Minh" },
            new List<string> { "Bình Thủy", "Cái Răng", "Ninh Kiều", "Ô Môn", "Thốt Nốt", "Cờ Đỏ", "Phong Điền", "Thới Lai", "Vĩnh Thạnh" },
            new List<string> { "Bảo Lạc", "Bảo Lâm", "Cao Bằng", "Hạ Lang", "Hà Quảng", "Hòa An", "Nguyên Bình", "Phục Hòa", "Quảng Uyên", "Thạch An", "Thông Nông", "Trà Lĩnh", "Trùng Khánh" },
            new List<string> { "Cẩm Lệ", "Hải Châu", "Liên Chiểu", "Ngũ Hành Sơn", "Sơn Trà", "Thanh Khê", "Hòa Vang", "Hoàng Sa" },
            new List<string> { "Buôn Đôn", "Buôn Hồ", "Buôn Ma Thuột", "Cư M'gar", "Cư Kuin", "Ea H'leo", "Ea Kar", "Ea Súp", "Krông Ana", "Krông Bông", "Krông Buk", "Krông Năng", "Krông Pắk", "Lắk", "M'Đrăk" },
            new List<string> { "Cư Jút", "Đắk Glong", "Đắk Mil", "Đắk R'Lấp", "Đắk Song", "Gia Nghĩa", "Krông Nô", "Tuy Đức" },
            new List<string> { "Điện Biên", "Điện Biên Đông", "Điện Biên Phủ", "Mường Ảng", "Mường Chà", "Mường Lay", "Mường Nhé", "Nậm Pồ", "Tủa Chùa", "Tuần Giáo" },
            new List<string> { "Biên Hòa", "Cẩm Mỹ", "Định Quán", "Long Khánh", "Long Thành", "Nhơn Trạch", "Tân Phú", "Thống Nhất", "Trảng Bom", "Vĩnh Cữu", "Xuân Lộc" },
            new List<string> { "Cao Lãnh", "Châu Thành", "Hồng Ngự", "Hồng Ngự", "Lai Vung", "Lấp Vò", "Sa Đéc", "Tam Nông", "Tân Hồng", "Thanh Bình", "Tháp Mười" },
            new List<string> { "Ayun Pa", "An Khê", "Chư Păh", "Chư Prông", "Chư Pưh", "Chư Sê", "Đắk Đoa", "Đắk Pơ", "Đức Cơ", "Ia Grai", "Ia Pa", "K'Bang", "Kông Chro", "Krông Pa", "Mang Yang", "Phú Thiện", "Pleiku" },
            new List<string> { "Bắc Mê", "Bắc Quang", "Đồng Văn", "Hà Giang", "Hoàng Su Phì", "Mèo Vạc", "Quản Bạ", "Quảng Bình", "Vị Xuyên", "Xín Mần", "Yên Minh" },
            new List<string> { "Bình Lục", "Duy Tiên", "Kim Bảng", "Lý Nhân", "Phủ Lý", "Thanh Liêm" },
            new List<string> { "Ba Đình", "Bắc Từ Liêm", "Cầu Giấy", "Đống Đa", "Hà Đông", "Hai Bà Trưng", "Hoàn Kiếm", "Hoàng Mai", "Nam Từ Liêm", "Tây Hồ", "Thanh Xuân", "Sơn Tây", "Từ Liêm", "Ba Vì", "Chương Mỹ", "Đan Phượng", "Đông Anh", "Gia Lâm", "Hoài Đức", "Mê Linh", "Mỹ Đức", "Long Biên", "Phú Xuyên", "Phúc Thọ", "Quốc Oai", "Sóc Sơn", "Thanh Oai", "Thạch Thất", "Thanh Trì", "Thường Tín", "Ứng Hòa" },
            new List<string> { "Cẩm Xuyên", "Can Lộc", "Đức Thọ", "Hà Tĩnh", "Hồng Lĩnh", "Hương Khê", "Hương Sơn", "Kỳ Anh", "Kỳ Anh", "Lộc Hà", "Nghi Xuân", "Thạch Hà", "Vũ Quang" },
            new List<string> { "Bình Giang", "Cẩm Giàng", "Chí Linh", "Gia Lộc", "Hải Dương", "Kim Thành", "Kinh Môn", "Nam Sách", "Ninh Giang", "Thanh Hà", "Thanh Miện", "Tứ Kỳ" },
            new List<string> { "Đồ Sơn", "Dương Kinh", "Hải An", "Hồng Bàng", "Kiến An", "Lê Chân", "Ngô Quyền", "An Dương", "An Lão", "Bạch Long Vĩ", "Cát Hải", "Kiến Thuỵ", "Thủy Nguyên", "Tiên Lãng", "Vĩnh Bảo" },
            new List<string> { "Châu Thành", "Châu Thành A", "Long Mỹ", "Long Mỹ", "Ngã Bảy", "Phụng Hiệp", "Vị Thanh", "Vị Thủy" },
            new List<string> { "Quận 1", "Quận 2", "Quận 3", "Quận 4", "Quận 5", "Quận 6", "Quận 7", "Quận 8", "Quận 9", "Quận 10", "Quận 11", "Quận 12", "Bình Tân", "Bình Thạnh", "Gò Vấp", "Phú Nhuận", "Tân Bình", "Tân Phú", "Thủ Đức", "Bình Chánh", "Cần Giờ", "Củ Chi", "Hóc Môn", "Nhà Bè" },
            new List<string> { "Cao Phong", "Đà Bắc", "Hòa Bình", "Kim Bôi", "Kỳ Sơn", "Lạc Sơn", "Lạc Thủy", "Lương Sơn", "Mai Châu", "Tân Lạc", "Yên Thủy" },
            new List<string> { "Ân Thi", "Hưng Yên", "Khoái Châu", "Kim Động", "Mỹ Hào", "Phù Cừ", "Tiên Lữ", "Văn Giang", "Văn Lâm", "Yên Mỹ" },
            new List<string> { "Cam Lâm", "Cam Ranh", "Diên Khánh", "Khánh Sơn", "Khánh Vĩnh", "Nha Trang", "Ninh Hòa", "Trường Sa", "Vạn Ninh" },
            new List<string> { "An Biên", "An Minh", "Châu Thành", "Giang Thành", "Giồng Riềng", "Gò Quao", "Hà Tiên", "Hòn Đất", "Kiên Hải", "Kiên Lương", "Phú Quốc", "Rạch Giá", "Tân Hiệp", "Vĩnh Thuận", "U Minh Thượng" },
            new List<string> { "Đắk Glei", "Đắk Hà", "Đắk Tô", "Ia H'Drai", "Kon Plông", "Kon Rẫy", "Kon Tum", "Ngọc Hồi", "Sa Thầy", "Tu Mơ Rông" },
            new List<string> { "Lai Châu", "Mường Tè", "Nậm Nhùn", "Phong Thổ", "Sìn Hồ", "Tam Đường", "Than Uyên", "Tân Uyên" },
            new List<string> { "Bảo Lâm", "Bảo Lộc", "Cát Tiên", "Đạ Huoai", "Đà Lạt", "Đạ Tẻh", "Đam Rông", "Di Linh", "Đơn Dương", "Đức Trọng", "Lạc Dương", "Lâm Hà" },
            new List<string> { "Bắc Sơn", "Bình Gia", "Cao Lộc", "Chi Lăng", "Đình Lập", "Hữu Lũng", "Lạng Sơn", "Lộc Bình", "Tràng Định", "Văn Lãng", "Văn Quân" },
            new List<string> { "Bắc Hà", "Bảo Thắng", "Bảo Yên", "Bát Xát", "Lào Cai", "Mường Khương", "Sa Pa", "Si Ma Cai", "Văn Bàn" },
            new List<string> { "Bến Lức", "Cần Đước", "Cần Giuộc", "Châu Thành", "Đức Hòa", "Đức Huệ", "Kiến Tường", "Mộc Hóa", "Tân An", "Tân Hưng", "Tân Thạnh", "Tân Trụ", "Thạnh Hóa", "Thủ Thừa", "Vĩnh Hưng" },
            new List<string> { "Giao Thủy", "Hải Hậu", "Mỹ Lộc", "Nam Định", "Nam Trực", "Nghĩa Hưng", "Trực Ninh", "Vụ Bản", "Xuân Trường", "Ý Yên" },
            new List<string> { "Anh Sơn", "Con Cuông", "Cửa Lò", "Diễn Châu", "Đô Lương", "Hoàng Mai", "Hưng Nguyên", "Kỳ Sơn", "Nam Đàn", "Nghi Lộc", "Nghĩa Đàn", "Quế Phong", "Quỳ Châu", "Quỳ Hợp", "Quỳnh Lưu", "Tân Kỳ", "Thanh Chương", "Tương Dương", "Vinh", "Yên Thành", "Thái Hòa" },
            new List<string> { "Gia Viễn", "Hoa Lư", "Kim Sơn", "Nho Quan", "Ninh Bình", "Tam Diệp", "Yên Khánh", "Yên Mô" },
            new List<string> { "Bác Ái", "Ninh Hải", "Ninh Phước", "Ninh Sơn", "Phan Rang–Tháp Chàm", "Thuận Bắc", "Thuận Nam" },
            new List<string> { "Cẩm Khê", "Đoan Hùng", "Hạ Hòa", "Lâm Thao", "Phù Ninh", "Phú Thọ", "Tam Nông", "Tân Sơn", "Thanh Ba", "Thanh Sơn", "Thanh Thủy", "Việt Trì", "Yên Lập" },
            new List<string> { "Đông Hòa", "Đồng Xuân", "Phú Hòa", "Sơn Hòa", "Sông Cầu", "Sông Hinh", "Tây Hòa", "Tuy An", "Tuy Hòa" },
            new List<string> { "Ba Đồn", "Bố Trạch", "Đồng Hới", "Lệ Thủy", "Minh Hóa", "Quảng Ninh", "Quảng Trạch", "Tuyên Hóa" },
            new List<string> { "Bắc Trà My", "Đại Lộc", "Điện Bàn", "Đông Giang", "Duy Xuyên", "Hiệp Đức", "Hội An", "Nam Giang", "Nam Trà My", "Nông Sơn", "Núi Thành", "Phước Sơn", "Quế Sơn", "Tam Kỳ", "Tây Giang", "Thăng Bình", "Tiên Phước" },
            new List<string> { "Ba Tơ", "Bình Sơn", "Đức Phổ", "Lý Sơn", "Minh Long", "Mộ Đức", "Nghĩa Hành", "Quảng Ngãi", "Sơn Hà", "Sơn Tây", "Sơn Tịnh", "Tây Trà", "Trà Bồng", "Tư Nghĩa" },
            new List<string> { "Ba Chẽ", "Bình Liêu", "Cẩm Phả", "Cô Tô", "Đầm Hà", "Đông Triều", "Hạ Long", "Hải Hà", "Hoành Bồ", "Móng Cái", "Quảng Yên", "Tiên Yên", "Uông Bí", "Vân Đồn" },
            new List<string> { "Cam Lộ", "Cồn Cỏ", "Đa Krông", "Đông Hà", "Gio Linh", "Hải Lăng", "Hướng Hóa", "Quảng Trị", "Triệu Phong", "Vĩnh Linh" },
            new List<string> { "Châu Thành", "Cù Lao Dung", "Long Phú", "Kế Sách", "Mỹ Tú", "Mỹ Xuyên", "Ngã Năm", "Sóc Trăng", "Thạnh Trị", "Trần Đề", "Vĩnh Châu" },
            new List<string> { "Bắc Yên", "Mai Sơn", "Mộc Châu", "Mường La", "Phù Yên", "Quỳnh Nhai", "Sơn La", "Sông Mã", "Sốp Cộp", "Thuận Châu", "Vân Hồ", "Yên Châu" },
            new List<string> { "Bến Cầu", "Châu Thành", "Dương Minh Châu", "Gò Dầu", "Hòa Thành", "Tân Biên", "Tân Châu", "Tây Ninh", "Trảng Bàng" },
            new List<string> { "Đông Hưng", "Hưng Hà", "Kiến Xương", "Quỳnh Phụ", "Thái Bình", "Thái Thụy", "Tiền Hải", "Vũ Thư" },
            new List<string> { "Đại Từ", "Định Hóa", "Đồng Hỷ", "Phổ Yên", "Phú Bình", "Phú Lương", "Sông Công", "Thái Nguyên", "Võ Nhai" },
            new List<string> { "Bá Thước", "Bỉm Sơn", "Cẩm Thủy", "Đông Sơn", "Hà Trung", "Hậu Lộc", "Hoằng Hóa", "Lang Chánh", "Mường Lát", "Nga Sơn", "Ngọc Lặc", "Như Thanh", "Như Xuân", "Nông Cống", "Quan Hóa", "Quan Sơn", "Quảng Xương", "Sầm Sơn", "Thạch Thành", "Thanh Hóa", "Thiệu Hóa", "Thọ Xuân", "Thường Xuân", "Tĩnh Gia", "Triệu Sơn", "Vĩnh Lộc", "Yên Định" },
            new List<string> { "A Lưới", "Huế", "Hương Thủy", "Hương Trà", "Nam Đông", "Phong Điền", "Phú Lộc", "Phú Vang", "Quảng Điền" },
            new List<string> { "Cái Bè", "Cai Lậy", "Cai Lậy", "Châu Thành", "Chợ Gạo", "Gò Công", "Gò Công Dông", "Gò Công Tây", "Mỹ Tho", "Tân Phú Đông", "Tân Phước" },
            new List<string> { "Càng Long", "Cầu Kè", "Cầu Ngang", "Châu Thành", "Duyên Hải", "Duyên Hải", "Tiểu Cần", "Trà Cú", "Trà Vinh" },
            new List<string> { "Chiêm Hóa", "Hàm Yên", "Lâm Bình", "Nà Hang", "Sơn Dương", "Tuyên Quang", "Yên Sơn" },
            new List<string> { "Bình Minh", "Bình Tân", "Long Hồ", "Mang Thít", "Tâm Bình", "Trà Ôn", "Vĩnh Long", "Vũng Liêm" },
            new List<string> { "Bình Xuyên", "Lập Thạch", "Phúc Yên", "Sông Lô", "Tam Đảo", "Tam Dương", "Vĩnh Tường", "Vĩnh Yên", "Yên Lạc" },
            new List<string> { "Lục Yên", "Mù Cang Chải", "Nghĩa Lộ", "Trạm Tấu", "Trấn Yên", "Văn Chấn", "Văn Yên", "Yên Bái", "Yên Bình" }
        };
        #endregion

        public override void Generate()
        {
            int cityId = 1, count = 0;

            List<District> districts = new List<District>();

            foreach (var city in _districts)
            {
                foreach (var district in city)
                {
                    districts.Add(new District
                    {
                        Id = ++count,
                        CityId = cityId,
                        Name = district
                    });
                }
                ++cityId;
            }

            districts.ForEach(x => _context.Districts.AddOrUpdate(p => p.Id, x));
            _context.SaveChanges();
        }
    }
}