using BTLW_BDT.Models;
using System.Collections.Generic;
namespace BTLW_BDT.ViewModels
{
    public class ProductDetailViewModel
    {
        public SanPham dmSp { get; set; } = new SanPham();
    
        public List<AnhSanPham> dmAnhSp { get; set; } = new List<AnhSanPham>();
        public List<MauSac> dmMauSp { get; set; } = new List<MauSac>();
        public List<Rom> dmRomSp { get; set; } = new List<Rom>();
        public string? SelectedColor { get; set; }
        public string? SelectedRom { get; set; }
        public decimal? CurrentPrice { get; set; }
        public List<DanhGium> Reviews { get; set; } = new List<DanhGium>();
        public double AverageRate { get; set; }
    }
}
