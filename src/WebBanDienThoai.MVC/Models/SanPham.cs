using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class SanPham
{
    public string MaSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public int? ThoiGianBaoHanh { get; set; }

    public int? SoLuongTonKho { get; set; }

    public decimal? DonGiaBanGoc { get; set; }

    public decimal? DonGiaBanRa { get; set; }

    public decimal? KhuyenMai { get; set; }

    public string? DanhBa { get; set; }

    public string? DenFlash { get; set; }

    public string? CongNgheManHinh { get; set; }

    public int? DoSangToiDa { get; set; }

    public string? LoaiPin { get; set; }

    public string? BaoMatNangCao { get; set; }

    public string? GhiAmMacDinh { get; set; }

    public string? JackTaiNghe { get; set; }

    public string? MangDiDong { get; set; }

    public string? Sim { get; set; }

    public string? MaHang { get; set; }

    public string? ManHinh { get; set; }

    public string? Pin { get; set; }

    public string? Camera { get; set; }

    public string? KichThuoc { get; set; }

    public string? Chip { get; set; }

    public string? Ram { get; set; }

    public string? AnhDaiDien { get; set; }

    public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; } = new List<AnhSanPham>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; } = new List<ChiTietHoaDonBan>();

    public virtual Hang? MaHangNavigation { get; set; }

    public virtual ICollection<MauSac> MauSacs { get; set; } = new List<MauSac>();

    public virtual ICollection<Rom> Roms { get; set; } = new List<Rom>();
}
