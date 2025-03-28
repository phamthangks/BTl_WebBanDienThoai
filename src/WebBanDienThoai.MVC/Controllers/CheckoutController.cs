using Microsoft.AspNetCore.Mvc;
using BTLW_BDT.Models.Cart;
using BTLW_BDT.Models;
using Microsoft.AspNetCore.Authorization;
using BTLW_BDT.Services;
using BTLW_BDT.Models.VnPay;
using BTLW_BDT.Models.Order;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BTLW_BDT.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly BtlLtwQlbdtContext _context;
        private readonly IVnPayService _vnPayService;

        public CheckoutController(BtlLtwQlbdtContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }

        public IActionResult DetailCheckout()
        {
            string userId = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Access");
            }

            var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
            if (gioHang == null)
            {
                return RedirectToAction("DetailCart", "Cart");
            }

            var chiTietGioHang = _context.ChiTietGioHangs
                .Include(c => c.MaGioHangNavigation)
                .Include(c => c.MaSanPhamNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .ToList();

            ViewData["Page"] = "Checkout";
            return View(chiTietGioHang);
        }

        [HttpPost]
        public IActionResult PlaceOrder(string paymentMethod, string ghiChu, string DiaChiGiaoHang, 
            decimal DiaChiLatitude, decimal DiaChiLongitude, decimal PhiGiaoHang)
        {
            string userId = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Access");
            }

            // Lưu ghi chú vào session để sử dụng sau này
            if (!string.IsNullOrEmpty(ghiChu))
            {
                HttpContext.Session.SetString("GhiChu", ghiChu);
            }

            var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
            if (gioHang == null || !_context.ChiTietGioHangs.Any(c => c.MaGioHang == gioHang.MaGioHang))
            {
                return RedirectToAction("DetailCart", "Cart");
            }

            var chiTietGioHang = _context.ChiTietGioHangs
                .Include(c => c.MaSanPhamNavigation)
                .Where(c => c.MaGioHang == gioHang.MaGioHang)
                .ToList();

            if (paymentMethod == "Pay at store")
            {
                return ProcessStorePayment(chiTietGioHang, userId);
            }
            else
            {
                return ProcessVnPayPayment(chiTietGioHang);
            }
        }

        private IActionResult ProcessStorePayment(List<ChiTietGioHang> chiTietGioHang, string userId)
        {
            try
            {
                var khachHang = _context.KhachHangs.FirstOrDefault(k => k.TenDangNhap == userId);
                if (khachHang == null)
                {
                    TempData["Error"] = "Không tìm thấy thông tin khách hàng";
                    return RedirectToAction("DetailCart", "Cart");
                }
                // Cập nhật ghi chú từ session
                var ghiChu = HttpContext.Session.GetString("GhiChu");
                if (!string.IsNullOrEmpty(ghiChu))
                {
                    khachHang.GhiChu = ghiChu;
                    _context.KhachHangs.Update(khachHang);
                }

                var newMaHoaDon = GenerateNewOrderId();
                var totalAmount = CalculateTotal(chiTietGioHang);

                var order = new HoaDonBan
                {
                    MaHoaDon = newMaHoaDon,
                    PhuongThucThanhToan = "Pay at store",
                    TongTien = totalAmount,
                    ThoiGianLap = DateTime.Now,
                    MaKhachHang = khachHang.MaKhachHang,
                    TrangThai = "chưa thanh toán",
                    DiaChiGiaoHang = Request.Form["DiaChiGiaoHang"].ToString(),
                    PhiGiaoHang = decimal.Parse(Request.Form["PhiGiaoHang"].ToString()),
                    DiaChiLatitude = decimal.Parse(Request.Form["DiaChiLatitude"].ToString()),
                    DiaChiLongtitude = decimal.Parse(Request.Form["DiaChiLongitude"].ToString()),
                    GhiChuHd = Request.Form["ghiChu"].ToString(),
                    TrangThaiGiaoHang = false
                };

                _context.HoaDonBans.Add(order);
                _context.SaveChanges();

                // Lưu thông tin chi tiết giỏ hàng vào session trước khi xử lý đơn hàng
                var cartDetails = new List<CartDetailTemp>();
                foreach (var item in chiTietGioHang)
                {
                    cartDetails.Add(new CartDetailTemp
                    {
                        MaSanPham = item.MaSanPham,
                        ThongSoMau = item.ThongSoMau,
                        ThongSoRom = item.ThongSoRom,
                        SoLuong = int.Parse(item.SoLuong.ToString())


                    });
                }
                HttpContext.Session.SetString("TempCartDetails", JsonSerializer.Serialize(cartDetails));

                foreach (var item in chiTietGioHang)
                {
                    var sanPham = item.MaSanPhamNavigation;
                    var danhSachRom = _context.Roms.Where(r => r.MaSanPham == item.MaSanPham).ToList();
                    var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
                    var selectedRom = _context.Roms.FirstOrDefault(r => r.MaRom == item.ThongSoRom);

                    decimal baseRomGia = baseRom?.Gia.GetValueOrDefault() ?? 0;
                    decimal selectedRomGia = selectedRom?.Gia.GetValueOrDefault() ?? 0;
                    decimal donGia = (selectedRomGia - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

                    var orderDetail = new ChiTietHoaDonBan
                    {
                        MaChiTietHoaDonBan = Guid.NewGuid().ToString(),
                        MaHoaDon = order.MaHoaDon,
                        MaSanPham = item.MaSanPham,
                        SoLuongBan = item.SoLuong ?? 0,
                        DonGiaCuoi = donGia
                    };

                    
                    _context.ChiTietHoaDonBans.Add(orderDetail);
                }

                UpdateInventory(chiTietGioHang);
                ClearCart(userId);

                _context.SaveChanges();

                HttpContext.Session.SetString("MaKhachHang", khachHang.MaKhachHang);

                TempData["Message"] = "Đặt hàng thành công - Thanh toán tại cửa hàng";
                return RedirectToAction("OrderSuccess");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi xử lý đơn hàng: " + ex.Message;
                return RedirectToAction("DetailCart", "Cart");
            }
        }

        private IActionResult ProcessVnPayPayment(List<ChiTietGioHang> chiTietGioHang)
        {
            try
            {
                // Lưu thông tin giao hàng vào TempData để sử dụng sau khi thanh toán
                TempData["DiaChiGiaoHang"] = Request.Form["DiaChiGiaoHang"].ToString();
                TempData["PhiGiaoHang"] = Request.Form["PhiGiaoHang"].ToString();
                TempData["DiaChiLatitude"] = Request.Form["DiaChiLatitude"].ToString();
                TempData["DiaChiLongitude"] = Request.Form["DiaChiLongitude"].ToString();
                TempData["GhiChuHd"] = Request.Form["ghiChu"].ToString();

                var totalAmount = CalculateTotal(chiTietGioHang);

                TempData["PaymentAmount"] = totalAmount.ToString();

                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = (double)totalAmount,
                    CreatedDate = DateTime.Now,
                    Description = "Thanh toán đơn hàng",
                    FullName = HttpContext.Session.GetString("HoTen") ?? "Khách hàng",
                    OrderId = new Random().Next(1000, 100000)
                };

                var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi xử lý thanh toán: " + ex.Message;
                return RedirectToAction("DetailCart", "Cart");
            }
        }

        private string GenerateNewOrderId()
        {
            Random random = new Random();
            string randomNumbers = random.Next(1000, 9999).ToString(); // Tạo số ngẫu nhiên 4 chữ số
            return "HD" + randomNumbers;
        }

        private decimal CalculateTotal(List<ChiTietGioHang> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                var sanPham = item.MaSanPhamNavigation;
                var danhSachRom = _context.Roms.Where(r => r.MaSanPham == item.MaSanPham).ToList();
                var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
                var selectedRom = _context.Roms.FirstOrDefault(r => r.MaRom == item.ThongSoRom);

                decimal baseRomGia = baseRom?.Gia.GetValueOrDefault() ?? 0;
                decimal selectedRomGia = selectedRom?.Gia.GetValueOrDefault() ?? 0;
                decimal donGia = (selectedRomGia - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

                total += donGia * (item.SoLuong ?? 0);
            }
            return total;
        }

        private void UpdateInventory(List<ChiTietGioHang> items)
        {
            foreach (var item in items)
            {
                var product = _context.SanPhams.Find(item.MaSanPham);
                if (product != null)
                {
                    product.SoLuongTonKho = product.SoLuongTonKho - (item.SoLuong ?? 0);
                }
            }
            _context.SaveChanges();
        }

        private void ClearCart(string userId)
        {
            var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
            if (gioHang != null)
            {
                var chiTietGioHang = _context.ChiTietGioHangs.Where(c => c.MaGioHang == gioHang.MaGioHang);
                _context.ChiTietGioHangs.RemoveRange(chiTietGioHang);
                gioHang.TongTien = 0;
                _context.SaveChanges();
            }

           
        }

        public IActionResult PaymentCallback()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                if (response == null || response.VnPayResponseCode != "00")
                {
                    TempData["Message"] = $"Lỗi thanh toán VN Pay: {response?.VnPayResponseCode}";
                    return RedirectToAction("PaymentFail");
                }

                string userId = HttpContext.Session.GetString("Username");
                var khachHang = _context.KhachHangs.FirstOrDefault(k => k.TenDangNhap == userId);
                if (khachHang == null)
                {
                    TempData["Error"] = "Không tìm thấy thông tin khách hàng";
                    return RedirectToAction("DetailCart", "Cart");
                }

                var newMaHoaDon = GenerateNewOrderId();

                var totalAmountStr = TempData["PaymentAmount"]?.ToString();
                if (!decimal.TryParse(totalAmountStr, out decimal totalAmount))
                {
                    TempData["Error"] = "Lỗi xử lý số tiền thanh toán";
                    return RedirectToAction("DetailCart", "Cart");
                }

                var order = new HoaDonBan
                {
                    MaHoaDon = newMaHoaDon,
                    PhuongThucThanhToan = "Bank transfer via QR code",
                    TongTien = totalAmount,
                    ThoiGianLap = DateTime.Now,
                    MaKhachHang = khachHang.MaKhachHang,
                    TrangThai = "đã thanh toán",
                    DiaChiGiaoHang = TempData["DiaChiGiaoHang"]?.ToString(),
                    PhiGiaoHang = decimal.Parse(TempData["PhiGiaoHang"]?.ToString() ?? "0"),
                    DiaChiLatitude = decimal.Parse(TempData["DiaChiLatitude"]?.ToString() ?? "0"),
                    DiaChiLongtitude = decimal.Parse(TempData["DiaChiLongitude"]?.ToString() ?? "0"),
                    GhiChuHd = TempData["GhiChuHd"]?.ToString(),
                    TrangThaiGiaoHang = false
                };

                _context.HoaDonBans.Add(order);
                _context.SaveChanges();



                var gioHang = _context.GioHangs.FirstOrDefault(g => g.TenDangNhap == userId);
                if (gioHang != null)
                {
                    var chiTietGioHang = _context.ChiTietGioHangs
                        .Include(c => c.MaSanPhamNavigation)
                        .Where(c => c.MaGioHang == gioHang.MaGioHang)
                        .ToList();

                    // Lưu thông tin chi tiết giỏ hàng vào session trước khi xử lý đơn hàng
                    var cartDetails = new List<CartDetailTemp>();
                    foreach (var item in chiTietGioHang)
                    {
                        cartDetails.Add(new CartDetailTemp
                        {
                            MaSanPham = item.MaSanPham,
                            ThongSoMau = item.ThongSoMau,
                            ThongSoRom = item.ThongSoRom,
                            SoLuong = int.Parse(item.SoLuong.ToString())

                        });
                    }
                    HttpContext.Session.SetString("TempCartDetails", JsonSerializer.Serialize(cartDetails));


                    foreach (var item in chiTietGioHang)
                    {
                        var sanPham = item.MaSanPhamNavigation;
                        var danhSachRom = _context.Roms.Where(r => r.MaSanPham == item.MaSanPham).ToList();
                        var baseRom = danhSachRom.OrderBy(r => r.Gia).FirstOrDefault();
                        var selectedRom = _context.Roms.FirstOrDefault(r => r.MaRom == item.ThongSoRom);

                        decimal baseRomGia = baseRom?.Gia.GetValueOrDefault() ?? 0;
                        decimal selectedRomGia = selectedRom?.Gia.GetValueOrDefault() ?? 0;
                        decimal donGia = (selectedRomGia - baseRomGia) + sanPham.DonGiaBanRa.GetValueOrDefault();

                        var orderDetail = new ChiTietHoaDonBan
                        {
                            MaChiTietHoaDonBan = Guid.NewGuid().ToString(),
                            MaHoaDon = order.MaHoaDon,
                            MaSanPham = item.MaSanPham,
                            SoLuongBan = item.SoLuong ?? 0,
                            DonGiaCuoi = donGia
                        };
                        _context.ChiTietHoaDonBans.Add(orderDetail);
                    }

                    UpdateInventory(chiTietGioHang);
                    ClearCart(userId);
                }

                _context.SaveChanges();

                HttpContext.Session.SetString("MaKhachHang", khachHang.MaKhachHang);

                TempData["Message"] = "Thanh toán QR thành công";
                return RedirectToAction("OrderSuccess");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi xử lý thanh toán: " + ex.Message;
                return RedirectToAction("PaymentFail");
            }
        }

        public IActionResult OrderSuccess()
        {
            // Debug: Kiểm tra tất cả các session
            var allSessionKeys = HttpContext.Session.Keys;
            var sessionDebugInfo = new Dictionary<string, string>();

            foreach (var key in allSessionKeys)
            {
                sessionDebugInfo[key] = HttpContext.Session.GetString(key);
            }

            // Debug: Kiểm tra session TempCartDetails
            var tempCartDetailsJson = HttpContext.Session.GetString("TempCartDetails");
            TempData["SessionDebug"] = $"TempCartDetails Content: {tempCartDetailsJson ?? "null"}";
            TempData["AllSessions"] = JsonSerializer.Serialize(sessionDebugInfo);

            string userId = HttpContext.Session.GetString("MaKhachHang");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "UserId is null or empty";
                return RedirectToAction("Login", "Access");
            }

            var lastOrder = _context.HoaDonBans
                .Include(h => h.ChiTietHoaDonBans)
                .ThenInclude(c => c.MaSanPhamNavigation)
                .Where(h => h.MaKhachHang == userId)
                .OrderByDescending(h => h.ThoiGianLap)
                .FirstOrDefault();

            if (lastOrder == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng";
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrEmpty(tempCartDetailsJson))
            {
                try
                {
                    var cartDetails = JsonSerializer.Deserialize<List<CartDetailTemp>>(tempCartDetailsJson);
                    ViewBag.CartDetails = cartDetails;
                    TempData["CartDetailsCount"] = cartDetails?.Count ?? 0;
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error deserializing cart details: {ex.Message}";
                }
            }
            else
            {
                TempData["Warning"] = "TempCartDetails is empty";
            }

            return View(lastOrder);

        }

        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}