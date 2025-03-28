using System;
using System.Collections.Generic;

namespace BTLW_BDT.Models;

public partial class Rom
{
    public string MaRom { get; set; } = null!;

    public string? ThongSo { get; set; }

    public decimal? Gia { get; set; }

    public string? MaSanPham { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
