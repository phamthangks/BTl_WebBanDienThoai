using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTLW_BDT.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonAPIController : ControllerBase
    {
        private readonly BtlLtwQlbdtContext _context;

        public HoaDonAPIController(BtlLtwQlbdtContext context)
        {
            _context = context;
        }
        [HttpGet("GetHoaDon")]
        public IActionResult GetAllHoaDon()
        {
            var dsHoaDon = _context.HoaDonBans
                .Include(hd => hd.ChiTietHoaDonBans)
                .ThenInclude(ct => ct.MaSanPhamNavigation) // nếu cần thêm thông tin sản phẩm
                .Include(hd => hd.MaKhachHangNavigation)
                .Select(hd => new
                {
                    MaHDB = hd.MaHoaDon,
                    Time = hd.ThoiGianLap,
                    KM = hd.KhuyenMai ?? 0,
                    PhiGH = hd.PhiGiaoHang ?? 0,
                    TT = hd.TongTien ?? 0,
                    PTTT = hd.PhuongThucThanhToan,
                    TenKH = hd.MaKhachHangNavigation.TenKhachHang,
                    SDT = hd.MaKhachHangNavigation.SoDienThoai,
                    DC = hd.MaKhachHangNavigation.DiaChi,
                    ProductDetails = hd.ChiTietHoaDonBans.Select(ct => new
                    {
                        MaSp = ct.MaSanPham,
                        SL = ct.SoLuongBan
                    }).ToList()
                })
                .OrderByDescending(x => x.Time)
                .ToList();

            return Ok(dsHoaDon);
        }


        [HttpGet("TimKiemHoaDon")]
        public IActionResult TimKiemHoaDon(string? searchQuery, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.HoaDonBans
                .Include(hd => hd.ChiTietHoaDonBans)
                .ThenInclude(ct => ct.MaSanPhamNavigation)
                .Include(hd => hd.MaKhachHangNavigation)
                .AsQueryable();

            // Tìm kiếm toàn cục
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(hd =>
                    hd.MaHoaDon.Contains(searchQuery) ||
                    hd.PhuongThucThanhToan.Contains(searchQuery) ||
                    hd.MaKhachHangNavigation.TenKhachHang.Contains(searchQuery) ||
                    hd.MaKhachHangNavigation.SoDienThoai.Contains(searchQuery) ||
                    hd.MaKhachHangNavigation.DiaChi.Contains(searchQuery));
            }

            // Lọc theo thời gian
            if (fromDate.HasValue)
            {
                query = query.Where(hd => hd.ThoiGianLap >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(hd => hd.ThoiGianLap <= toDate.Value);
            }

            var result = query.Select(hd => new
            {
                MaHDB = hd.MaHoaDon,
                Time = hd.ThoiGianLap,
                KM = hd.KhuyenMai ?? 0,
                PhiGH = hd.PhiGiaoHang ?? 0,
                TT = hd.TongTien ?? 0,
                PTTT = hd.PhuongThucThanhToan,
                TenKH = hd.MaKhachHangNavigation.TenKhachHang,
                SDT = hd.MaKhachHangNavigation.SoDienThoai,
                DC = hd.MaKhachHangNavigation.DiaChi,
                ProductDetails = hd.ChiTietHoaDonBans.Select(ct => new
                {
                    MaSp = ct.MaSanPham,
                    SL = ct.SoLuongBan
                }).ToList()
            }).ToList();

            return Ok(new
            {
                TotalCount = result.Count,
                Data = result
            });
        }


    }
}