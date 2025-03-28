using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class HoaDonBan
{
    public string MaHoaDon { get; set; } = null!;

    public string? PhuongThucThanhToan { get; set; }

    public decimal? TongTien { get; set; }

    public decimal? KhuyenMai { get; set; }

    public DateTime ThoiGianLap { get; set; }

    public string? MaNhanVien { get; set; }

    public string? MaKhachHang { get; set; }

    public string? TrangThai { get; set; }

    public decimal? PhiGiaoHang { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public bool? TrangThaiGiaoHang { get; set; }

    public decimal? DiaChiLatitude { get; set; }

    public decimal? DiaChiLongtitude { get; set; }

    public string? GhiChuHd { get; set; }

    public virtual ICollection<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; } = new List<ChiTietHoaDonBan>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }
}
