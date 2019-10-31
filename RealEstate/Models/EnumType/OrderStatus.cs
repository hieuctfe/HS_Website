namespace RealEstate.EnumType.Models
{
    using System.ComponentModel;

    public enum OrderStatus
    {
        [Description("Đang Giao Dịch")]
        InProcess = 0,
        [Description("Đã Xong")]
        Done = 1,
        [Description("Đã Hủy")]
        Cancel = 2,
    }
}