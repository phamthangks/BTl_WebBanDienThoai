namespace BTLW_BDT.Models.Order
{
    public class OrderModel
    {
        public string MaKhachHang { get; set; } = string.Empty;

        public string? TenKhachHang { get; set; }

        public DateOnly? NgaySinh { get; set; }

        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }

        public string? LoaiKhachHang { get; set; }
        public string? GhiChu { get; set; }
    }
}
