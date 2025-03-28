using BTLW_BDT.Models.Cart;
using BTLW_BDT.Models;
using BTLW_BDT.ViewModels;
using BTLW_BDT;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly BtlLtwQlbdtContext _context;

    public CartController(BtlLtwQlbdtContext context)
    {
        _context = context;
    }

    //public int GetCartCount(string userId)
    //{
    //    if (string.IsNullOrEmpty(userId)) return 0;

    //    var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
    //    if (gioHang == null) return 0;

    //    return _context.ChiTietGioHangs.Count(c => c.MaGioHang == gioHang.MaGioHang);
    //}

    public IActionResult Index()
    {
        return RedirectToAction("DetailCart");
    }

    public IActionResult DetailCart()
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Access");
        }

        var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
        if (gioHang == null)
        {
            return View(new List<ChiTietGioHang>());
        }

        var chiTietGioHang = _context.ChiTietGioHangs
            .Where(c => c.MaGioHang == gioHang.MaGioHang)
            .ToList();

       // ViewBag.CartCount = GetCartCount(userId);
        ViewData["Page"] = "Shopping Cart";

        return View(chiTietGioHang);
    }


    public IActionResult AddToCart(string id, decimal? currentPrice = null, string? maMau = null, string? maRom = null, int quantity = 1)
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Access");
        }

        var sanPham = _context.SanPhams.SingleOrDefault(x => x.MaSanPham == id);
        if (sanPham == null) return RedirectToAction("Index", "Home");

        // Lấy và kiểm tra màu sắc
        var danhSachMau = _context.MauSacs.Where(m => m.MaSanPham == id).ToList();
        var selectedMau = string.IsNullOrEmpty(maMau)
            ? danhSachMau.FirstOrDefault()
            : danhSachMau.FirstOrDefault(m => m.MaMau == maMau) ?? danhSachMau.FirstOrDefault();

        // Lấy và kiểm tra ROM
        var danhSachRom = _context.Roms.Where(r => r.MaSanPham == id).ToList();
        var selectedRom = string.IsNullOrEmpty(maRom)
            ? danhSachRom.FirstOrDefault()
            : danhSachRom.FirstOrDefault(r => r.MaRom == maRom) ?? danhSachRom.FirstOrDefault();

        if (selectedMau == null || selectedRom == null)
        {
            TempData["Error"] = "Không tìm thấy thông tin màu sắc hoặc ROM cho sản phẩm này";
            return RedirectToAction("Index", "Home");
        }

        // Khai báo gioHang ở ngoài để có thể dùng trong cả try và catch
        GioHang gioHang = null;

        try
        {
            // Kiểm tra và tạo giỏ hàng nếu chưa có
            gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaGioHang = Guid.NewGuid().ToString(),
                    TenDangNhap = userId,
                    TongTien = 0
                };
                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();
            }

            // Tính giá
            var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
            decimal baseRomGia = baseRom?.Gia.GetValueOrDefault() ?? 0;
            decimal selectedRomGia = selectedRom.Gia.GetValueOrDefault();
            decimal donGia = (selectedRomGia - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

            // Kiểm tra sản phẩm trong giỏ hàng
            var chiTietGioHang = _context.ChiTietGioHangs.Where(c =>
                c.MaGioHang == gioHang.MaGioHang &&
                c.MaSanPham == id &&
                c.ThongSoMau == selectedMau.MaMau &&
                c.ThongSoRom == selectedRom.MaRom
            ).FirstOrDefault();

            if (chiTietGioHang != null)
            {
                // Nếu đã có sản phẩm với cùng màu và ROM, tăng số lượng
                chiTietGioHang.SoLuong += quantity;
                Console.WriteLine($"Cập nhật số lượng cho sản phẩm hiện có: {sanPham.TenSanPham}, Màu: {selectedMau.TenMau}, ROM: {selectedRom.ThongSo}, Số lượng mới: {chiTietGioHang.SoLuong}");
            }
            else
            {
                // Nếu chưa có, tạo mới chi tiết giỏ hàng
                chiTietGioHang = new ChiTietGioHang
                {
                    MaChiTietGioHang = Guid.NewGuid().ToString(),
                    MaGioHang = gioHang.MaGioHang,
                    MaSanPham = id,
                    ThongSoMau = selectedMau.MaMau,
                    ThongSoRom = selectedRom.MaRom,
                    SoLuong = quantity
                };
                _context.ChiTietGioHangs.Add(chiTietGioHang);
                Console.WriteLine($"Thêm sản phẩm mới vào giỏ hàng: {sanPham.TenSanPham}, Màu: {selectedMau.TenMau}, ROM: {selectedRom.ThongSo}, Số lượng: {quantity}");
            }

            // Cập nhật tổng tiền
            gioHang.TongTien += donGia * quantity;

            // Lưu thay đổi
            _context.SaveChanges();
          //  ViewBag.CartCount = GetCartCount(userId);

            TempData["Success"] = "Thêm sản phẩm vào giỏ hàng thành công";
            return RedirectToAction("DetailCart");
        }
        catch (Exception ex)
        {
            // Log chi tiết hơn về lỗi
            Console.WriteLine($"Error in AddToCart: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            Console.WriteLine($"Stack trace: {ex.StackTrace}");

            // Log thông tin chi tiết về sản phẩm đang thêm
            Console.WriteLine($"\nThông tin sản phẩm đang thêm:");
            Console.WriteLine($"- Mã SP: {id}");
            Console.WriteLine($"- Tên SP: {sanPham?.TenSanPham}");
            Console.WriteLine($"- Màu: {selectedMau?.TenMau} ({selectedMau?.MaMau})");
            Console.WriteLine($"- ROM: {selectedRom?.ThongSo} ({selectedRom?.MaRom})");
            Console.WriteLine($"- Số lượng: {quantity}");

            // Log thông tin giỏ hàng hiện tại
            if (gioHang != null)
            {
                Console.WriteLine($"\nThông tin giỏ hàng hiện tại (MaGioHang: {gioHang.MaGioHang}):");
                var currentCartItems = _context.ChiTietGioHangs
                    .Where(c => c.MaGioHang == gioHang.MaGioHang)
                    .ToList();

                foreach (var item in currentCartItems)
                {
                    Console.WriteLine($"- MaSP: {item.MaSanPham}, Màu: {item.ThongSoMau}, ROM: {item.ThongSoRom}, SL: {item.SoLuong}");
                }
            }

            // Hiển thị thông báo lỗi chi tiết hơn cho người dùng
            TempData["Error"] = $"Có lỗi xảy ra khi thêm vào giỏ hàng: " +
                               $"{(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}. \n" +
                               $"Sản phẩm: {sanPham?.TenSanPham}, " +
                               $"Màu: {selectedMau?.TenMau}, " +
                               $"ROM: {selectedRom?.ThongSo}";

            return RedirectToAction("DetailCart");
        }
    }

    [HttpPost]
    public IActionResult RemoveFromCart(string maSanPham, string? maMau, string? maRom)
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Access");

        var gioHang = _context.GioHangs.SingleOrDefault(g => g.TenDangNhap == userId);
        if (gioHang == null) return RedirectToAction("DetailCart");

        var item = _context.ChiTietGioHangs.SingleOrDefault(c =>
            c.MaGioHang == gioHang.MaGioHang &&
            c.MaSanPham == maSanPham &&
            c.ThongSoMau == maMau &&
            c.ThongSoRom == maRom);

        if (item != null)
        {
            var rom = _context.Roms.SingleOrDefault(r => r.MaRom == maRom);
            var sanPham = _context.SanPhams.SingleOrDefault(s => s.MaSanPham == maSanPham);
            var danhSachRom = _context.Roms.Where(r => r.MaSanPham == maSanPham).ToList();
            var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
            var selectedRom = _context.Roms.SingleOrDefault(r => r.MaRom == maRom);

            decimal baseRomGia = baseRom.Gia.GetValueOrDefault();
            decimal donGia = (selectedRom.Gia.GetValueOrDefault() - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

            gioHang.TongTien -= donGia * item.SoLuong;
            _context.ChiTietGioHangs.Remove(item);
            // Cập nhật ViewBag.CartCount trước khi chuyển hướng
         //   ViewBag.CartCount = GetCartCount(userId);
            _context.SaveChanges();
        }
        else
        {
            return RedirectToAction("");

        }

        return RedirectToAction("DetailCart");
    }

    [HttpPost, HttpGet]
    public IActionResult UpdateQuantity(string maSanPham, string? maMau, string? maRom, string action)
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Access");

        var gioHang = _context.GioHangs.SingleOrDefault(g => g.TenDangNhap == userId);
        if (gioHang == null) return RedirectToAction("DetailCart");

        var item = _context.ChiTietGioHangs.SingleOrDefault(c =>
            c.MaGioHang == gioHang.MaGioHang &&
            c.MaSanPham == maSanPham &&
            c.ThongSoMau == maMau &&
            c.ThongSoRom == maRom);

        if (item != null)
        {
            var sanPham = _context.SanPhams.SingleOrDefault(s => s.MaSanPham == maSanPham);
            var danhSachRom = _context.Roms.Where(r => r.MaSanPham == maSanPham).ToList();
            var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
            var selectedRom = _context.Roms.SingleOrDefault(r => r.MaRom == maRom);

            decimal baseRomGia = baseRom.Gia.GetValueOrDefault();
            decimal donGia = (selectedRom.Gia.GetValueOrDefault() - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

            if (action == "increase")
            {
                item.SoLuong++;
                gioHang.TongTien += donGia;
            }
            else if (action == "decrease" && item.SoLuong > 1)
            {
                item.SoLuong--;
                gioHang.TongTien -= donGia;
            }

            _context.SaveChanges();
        }

        return RedirectToAction("DetailCart");
    }

    public IActionResult ClearCart()
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Access");

        var gioHang = _context.GioHangs.SingleOrDefault(g => g.TenDangNhap == userId);
        if (gioHang != null)
        {
            var chiTietGioHang = _context.ChiTietGioHangs.Where(c => c.MaGioHang == gioHang.MaGioHang);
            _context.ChiTietGioHangs.RemoveRange(chiTietGioHang);
            gioHang.TongTien = 0;
            ViewBag.CartCount = 0;
            _context.SaveChanges();
        }

        return RedirectToAction("DetailCart");
    }

    public IActionResult Checkout()
    {
        string userId = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login", "Access");
        }

        var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
        if (gioHang == null)
        {
            return RedirectToAction("DetailCart");
        }

        var chiTietGioHang = _context.ChiTietGioHangs
            .Where(c => c.MaGioHang == gioHang.MaGioHang)
            .ToList();

        if (!chiTietGioHang.Any())
        {
            return RedirectToAction("DetailCart");
        }

     //   ViewBag.CartCount = GetCartCount(userId);
        ViewData["Page"] = "Checkout";
        return View(chiTietGioHang);
    }
}