using BTLW_BDT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanDienThoai.API.Models;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieuDoAPI : ControllerBase
    {
        private readonly BtlLtwQlbdtContext _context;

        public BieuDoAPI(BtlLtwQlbdtContext context)
        {
            _context = context;
        }

        // API lấy thông tin thống kê
        [HttpGet("GetThongKe")]
        public IActionResult GetThongKe()
        {
            var result = new
            {
                CountProduct = _context.SanPhams.Count(),
                CountCTH_context = _context.ChiTietHoaDonBans.Count(),
                CountCTGH = _context.ChiTietGioHangs.Count()
            };

            return Ok(result);
        }

        // API lấy thông tin biểu đồ thống kê
        [HttpPost("GetChartData")]
        public IActionResult GetChartData([FromBody] DateTimeRange dateRange)
        {
            var data = _context.HoaDonBans
                .Where(h_context => h_context.ThoiGianLap >= dateRange.FromDate && h_context.ThoiGianLap <= dateRange.ToDate)
                .GroupJoin(
                    _context.ChiTietHoaDonBans.Join(_context.SanPhams,
                        cth => cth.MaSanPham,
                        sp => sp.MaSanPham,
                        (cth, sp) => new
                        {
                            cth.MaHoaDon,
                            DonGiaBanGoc = sp.DonGiaBanGoc,
                            DonGiaCuoi = cth.DonGiaCuoi,
                            SoLuongBan = cth.SoLuongBan
                        }),
                    h_context => h_context.MaHoaDon,
                    cth_sp => cth_sp.MaHoaDon,
                    (h_context, cth_sp) => new { h_context, cth_sp }
                )
                .Select(g => new
                {
                    date = g.h_context.ThoiGianLap.Date.ToString("yyyy-MM-dd"),
                    sold = g.cth_sp.Sum(cth => cth.SoLuongBan),
                    quantity = g.h_context.TongTien,
                    profit = g.h_context.TongTien - g.cth_sp
                        .Where(cth => cth.DonGiaCuoi != null) // Chỉ tính khi có DonGiaCuoi
                        .Sum(cth => cth.DonGiaBanGoc * cth.SoLuongBan)
                })
                .ToList();

            var totalRevenue = data.Sum(d => d.quantity); // Tổng doanh thu
            var totalProfit = data.Sum(d => d.profit);

            return Ok(new { data, totalRevenue, totalProfit });
        }
    }

    // Model cho khoảng thời gian
    public class DateTimeRange
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}