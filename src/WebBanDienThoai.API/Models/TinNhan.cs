using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class TinNhan
{
    public int MaTinNhan { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? ThoiGian { get; set; }

    public bool? TrangThai { get; set; }

    public string MaKhachHang { get; set; } = null!;

    public string? LoaiNguoiGui { get; set; }

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
