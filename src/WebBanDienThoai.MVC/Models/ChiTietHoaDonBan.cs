using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class ChiTietHoaDonBan
{
    public int? SoLuongBan { get; set; }

    public decimal? DonGiaCuoi { get; set; }

    public string MaHoaDon { get; set; } = null!;

    public string MaSanPham { get; set; } = null!;

    public string MaChiTietHoaDonBan { get; set; } = null!;

    public virtual HoaDonBan MaHoaDonNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
