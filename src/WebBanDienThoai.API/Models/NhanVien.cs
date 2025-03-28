using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string? TenNhanVien { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? ChucVu { get; set; }

    public string? GhiChu { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? TenDangNhap { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
