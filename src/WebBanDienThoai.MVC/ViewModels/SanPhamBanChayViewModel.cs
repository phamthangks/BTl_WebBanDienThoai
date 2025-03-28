namespace BTLW_BDT.ViewModels
{
    public class SanPhamBanChayViewModel
    {
        // Thông tin cơ bản sản phẩm
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal? DonGiaBanGoc { get; set; }
        public decimal? DonGiaBanRa { get; set; }
        public decimal? KhuyenMai { get; set; }
        public string AnhDaiDien { get; set; }
        public int? SoLuongTonKho { get; set; }
        
        // Thông tin thống kê
        public int TongSoLuongBan { get; set; }
        public decimal DoanhThu { get; set; }
    }
} 