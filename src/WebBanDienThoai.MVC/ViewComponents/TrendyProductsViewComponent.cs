using BTLW_BDT.Models;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BTLW_BDT.ViewComponents
{
    public class TrendyProductsViewComponent : ViewComponent
    {
        private readonly BtlLtwQlbdtContext _db;
        private readonly IMemoryCache _cache;

        public TrendyProductsViewComponent(BtlLtwQlbdtContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public IViewComponentResult Invoke(int itemCount = 8)
        {
            var sanPhamBanChay = GetSanPhamBanChay(itemCount);
            return View(sanPhamBanChay);
        }

        private List<SanPhamBanChayViewModel> GetSanPhamBanChay(int itemCount)
        {
            const string cacheKey = "SanPhamBanChay";

            var allProducts = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                
                var hasOrders = _db.ChiTietHoaDonBans.Any();

                if (hasOrders)
                {
                    var query = from sp in _db.SanPhams
                               join ct in _db.ChiTietHoaDonBans on sp.MaSanPham equals ct.MaSanPham
                               where ct.SoLuongBan > 0 && ct.DonGiaCuoi > 0
                               group new { sp, ct } by new 
                               { 
                                   sp.MaSanPham,
                                   sp.TenSanPham,
                                   sp.DonGiaBanGoc,
                                   sp.DonGiaBanRa,
                                   sp.KhuyenMai,
                                   sp.AnhDaiDien
                               } into g
                               select new SanPhamBanChayViewModel
                               {
                                   MaSanPham = g.Key.MaSanPham,
                                   TenSanPham = g.Key.TenSanPham,
                                   DonGiaBanGoc = g.Key.DonGiaBanGoc,
                                   DonGiaBanRa = g.Key.DonGiaBanRa,
                                   KhuyenMai = g.Key.KhuyenMai,
                                   AnhDaiDien = g.Key.AnhDaiDien,
                                   TongSoLuongBan = g.Sum(x => x.ct.SoLuongBan ?? 0),
                                   DoanhThu = g.Sum(x => (x.ct.SoLuongBan ?? 0) * (x.ct.DonGiaCuoi ?? 0))
                               };

                    var result = query.OrderByDescending(x => x.TongSoLuongBan)
                                   .ThenByDescending(x => x.DoanhThu)
                                   .Take(8)
                                   .ToList();

                    if (result.Any())
                    {
                        return result;
                    }
                }

                return _db.SanPhams
                        .OrderByDescending(x => x.DonGiaBanRa)
                        .Select(sp => new SanPhamBanChayViewModel
                        {
                            MaSanPham = sp.MaSanPham,
                            TenSanPham = sp.TenSanPham,
                            DonGiaBanGoc = sp.DonGiaBanGoc,
                            DonGiaBanRa = sp.DonGiaBanRa,
                            KhuyenMai = sp.KhuyenMai,
                            AnhDaiDien = sp.AnhDaiDien,
                            TongSoLuongBan = 0,
                            DoanhThu = 0
                        })
                        .ToList();
            });

            return allProducts.Take(itemCount).ToList();
        }
    }
} 