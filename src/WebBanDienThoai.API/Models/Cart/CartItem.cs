namespace BTLW_BDT.Models.Cart
{
    public class CartItem
    {
        public string MaSanPham { get; set; }

        public string TenSanPham { get; set; }

        public string Anh {  get; set; }    

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }
        public decimal? TongTien => CurrentPrice * SoLuong;

        // Thêm thông tin từ ProductDetailViewModel
        public string? MaMau { get; set; }
        public string? TenMau { get; set; }
        public string? MaRom { get; set; }
        public string? DungLuongRom { get; set; }
        public decimal? CurrentPrice { get; set; } // Giá hiện tại của cấu hình đã chọn


    }
}
