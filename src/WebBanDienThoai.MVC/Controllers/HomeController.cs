using BTLW_BDT.Models;
using BTLW_BDT.Models.Cart;
using Azure;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using X.PagedList.Extensions;

namespace BTLW_BDT.Controllers
{
    public class HomeController : Controller
    {
        BtlLtwQlbdtContext db = new BtlLtwQlbdtContext();

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMemoryCache cache)
        {

            _logger = logger;
            _configuration = configuration;
            _cache = cache;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }

        public int GetCartQuantity()
        {
            var userId = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userId))
                return 0;

            var gioHang = db.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
            if (gioHang == null)
                return 0;

            // Tính tổng số lượng sản phẩm trong giỏ hàng
            return db.ChiTietGioHangs
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .Sum(c => c.SoLuong) ?? 0;
        }


        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            return View();
        }

        public IActionResult SanPhamTheoHang(string mahang, int ? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.SanPhams.AsNoTracking().Where(x => x.MaHang == mahang).OrderBy(x => x.TenSanPham).ToList();

            PagedList<SanPham> lst = new PagedList<SanPham>(lstsanpham, pageNumber, pageSize);
            ViewBag.mahang = mahang;
            return View(lst);
        }

        public IActionResult Shop(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.SanPhams.AsNoTracking().OrderBy(x => x.TenSanPham);
            PagedList<SanPham> lst = new PagedList<SanPham>
                (lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult ProductDetail(string maSp, string? maMau = null, string? maRom = null)
        {
            var sanPham = db.SanPhams.SingleOrDefault(x => x.MaSanPham == maSp) ?? new SanPham();
            var anhSanPham = db.AnhSanPhams.Where(x => x.MaSanPham == maSp).ToList();
            var mauSanPham = db.MauSacs.Where(x => x.MaSanPham == maSp).ToList();
            var romSanPham = db.Roms.Where(x => x.MaSanPham == maSp)
                        .OrderBy(x => x.Gia)
                        .ToList();

            // Sử dụng màu được chọn từ giỏ hàng nếu có, nếu không thì lấy màu đầu tiên
            var selectedColor = !string.IsNullOrEmpty(maMau)
                ? maMau
                : mauSanPham.FirstOrDefault()?.MaMau;

            // Lấy ảnh theo màu được chọn
            var selectedColorImages = anhSanPham.Where(x => x.MaMau == selectedColor).ToList();

            // Sử dụng ROM được chọn từ giỏ hàng nếu có, nếu không thì lấy ROM đầu tiên
            var selectedRom = !string.IsNullOrEmpty(maRom)
                ? romSanPham.FirstOrDefault(r => r.MaRom == maRom)
                : romSanPham.FirstOrDefault();

            // Tính giá dựa trên ROM được chọn
            var baseRom = romSanPham.FirstOrDefault();
            decimal? currentPrice = null;
            if (selectedRom != null && baseRom != null)
            {
                currentPrice = sanPham.DonGiaBanRa + (selectedRom.Gia - baseRom.Gia);
            }

            var reviews = db.DanhGia.Where(r => r.MaSanPham == maSp).ToList();

            var detailView = new ProductDetailViewModel
            {
                dmSp = sanPham,
                dmAnhSp = selectedColorImages,  // Sử dụng ảnh của màu được chọn
                dmMauSp = mauSanPham,
                dmRomSp = romSanPham,
                SelectedColor = selectedColor,  // Đặt màu được chọn
                SelectedRom = selectedRom?.MaRom,  // Đặt ROM được chọn
                CurrentPrice = currentPrice ?? sanPham.DonGiaBanRa,
                Reviews = reviews
            };

            return View(detailView);
        }
        public IActionResult GetColorImages(string maSp, string maMau)
        {
            var anhSanPham = db.AnhSanPhams.Where(x => x.MaSanPham == maSp && x.MaMau == maMau).ToList();
            return PartialView("_ColorImagesPartial", anhSanPham);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetRomPrice(string maSp, string maRom)
        {
            var sanPham = db.SanPhams.SingleOrDefault(x => x.MaSanPham == maSp);
            var romList = db.Roms.Where(x => x.MaSanPham == maSp)
                    .OrderBy(x => x.Gia)
                    .ToList();
        
            var selectedRom = romList.FirstOrDefault(x => x.MaRom == maRom);
            var baseRom = romList.FirstOrDefault(); // ROM nhỏ nhất
        
            if (selectedRom == null || baseRom == null || sanPham == null)
                return Json(new { success = false });
            
            // Tính giá mới = Giá cơ bản + (Giá ROM đã chọn - Giá ROM nhỏ nhất)
            var newPrice = sanPham.DonGiaBanRa + (selectedRom.Gia - baseRom.Gia);
        
            string formattedPrice;

            if (newPrice.HasValue)
            {
                formattedPrice = newPrice.Value.ToString("#,##0") + " VNĐ";
            }
            else
            {
                formattedPrice = "Giá không khả dụng";
            }
        
            // Sử dụng formattedPrice ở đây
            return Json(new { 
                success = true, 
                price = newPrice,
                formattedPrice = formattedPrice
            });
        }
        public IActionResult Contact()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Access");
            }
            
            return View();
        }

        public IActionResult OrderHistory(int? page)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Access");
            }

            var customer = db.KhachHangs.FirstOrDefault(k => k.TenDangNhap == username);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var orders = db.HoaDonBans
                .Include(h => h.ChiTietHoaDonBans)
                .ThenInclude(c => c.MaSanPhamNavigation)
                .Where(h => h.MaKhachHang == customer.MaKhachHang)
                .OrderByDescending(h => h.ThoiGianLap)
                .ToPagedList(pageNumber, pageSize);

            return View(orders);
        }

    }
}
