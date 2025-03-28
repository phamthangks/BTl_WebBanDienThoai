using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class LichSuHoatDong
{
    public string MaHoatDong { get; set; } = null!;

    public string? LoaiHoatDong { get; set; }

    public DateTime? ThoiGianThucHien { get; set; }

    public string? GhiChu { get; set; }

    public string? TenDangNhap { get; set; }

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
