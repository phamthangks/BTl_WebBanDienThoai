using Azure;
using BTLW_BDT.Models;
using BTLW_BDT.Models.BieuDo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
using X.PagedList;
using X.PagedList.Extensions;
using System.Text.RegularExpressions;
using BTLW_BDT.Helpers;
using BTLW_BDT.ViewModels;
using System.Linq;

namespace BTLW_BDT.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    //[Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        BtlLtwQlbdtContext db = new BtlLtwQlbdtContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return RedirectToAction("DanhMucSanPham");
        }
        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham()
        {
            // Load the list of brands for the dropdown
            ViewBag.MaHang = new SelectList(db.Hangs.ToList(), "MaHang", "TenHang");

            return View();
        }

        [Route("DashBoard")]
        public IActionResult DashBoard()
        {
            return View();
        }

        [Route("QuanLyHoaDon")]
        public IActionResult QuanLyHoaDon(string searchQuery, int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;

            // Truyền giá trị searchQuery vào ViewData để sử dụng trong view
            ViewData["searchQuery"] = searchQuery;

            DateTime? searchDate = null;
            if (DateTime.TryParse(searchQuery, out DateTime parsedDate))
            {
                searchDate = parsedDate.Date; // Lấy chỉ phần ngày, bỏ giờ phút giây
            }

              // Dereference of a possibly null reference.
            var lstSanPham = from hdb in db.HoaDonBans
                             join kh in db.KhachHangs on hdb.MaKhachHang equals kh.MaKhachHang
                             where string.IsNullOrEmpty(searchQuery)
                                   || hdb.MaHoaDon.Contains(searchQuery)  // Tìm theo Mã Hóa Đơn
                                   || kh.TenKhachHang.Contains(searchQuery)  // Tìm theo Tên Khách Hàng
                                   || hdb.PhiGiaoHang.ToString().Contains(searchQuery)
                                   || hdb.PhuongThucThanhToan.Contains(searchQuery)  // Tìm theo Phương thức thanh toán
                                   || hdb.KhuyenMai.ToString().Contains(searchQuery)  // Tìm theo Khuyến Mại
                                   || hdb.TongTien.ToString().Contains(searchQuery)  // Tìm theo Tổng Tiền
                                   || (searchDate.HasValue && hdb.ThoiGianLap.Date == searchDate.Value)
                                   || db.ChiTietHoaDonBans.Where(cthdb => cthdb.MaHoaDon == hdb.MaHoaDon).Any(cthdb => cthdb.MaSanPham.Contains(searchQuery)) // Tìm theo Mã sản phẩm
                                   || kh.SoDienThoai.Contains(searchQuery)
                                   || hdb.DiaChiGiaoHang.Contains(searchQuery)
                                   || hdb.MaNhanVien.Contains(searchQuery)
                             select new
                             {
                                 MaHDB = hdb.MaHoaDon,
                                 PTTT = hdb.PhuongThucThanhToan,
                                 PhiGH = hdb.PhiGiaoHang,
                                 TT = hdb.TongTien,
                                 KM = hdb.KhuyenMai,
                                 Time = hdb.ThoiGianLap,
                                 MaNV = hdb.MaNhanVien,
                                 TenKH = kh.TenKhachHang,
                                 SDT = kh.SoDienThoai,
                                 DC = hdb.DiaChiGiaoHang,
                                 ProductDetails = db.ChiTietHoaDonBans
                                                    .Where(cthdb => cthdb.MaHoaDon == hdb.MaHoaDon)
                                                    .Select(cthdb => new
                                                    {
                                                        SL = cthdb.SoLuongBan,
                                                        MaSp = cthdb.MaSanPham
                                                    }).ToList()
                             };
            // Dereference of a possibly null reference.

            var pagedList = lstSanPham.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }



        [Route("Chat")]
        public async Task<IActionResult> Chat()
        {
            var customers = await db.KhachHangs
                .Include(k => k.TenDangNhapNavigation) // Include thông tin TaiKhoan
                .Include(k => k.TinNhans) // Include tin nhắn
                .Select(k => new KhachHang
                {
                    MaKhachHang = k.MaKhachHang,
                    TenKhachHang = k.TenKhachHang,
                    AnhDaiDien = k.AnhDaiDien,
                    TenDangNhapNavigation = k.TenDangNhapNavigation, // Map thông tin TaiKhoan
                    LastMessage = k.TinNhans
                        .OrderByDescending(t => t.ThoiGian)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return View(customers);
        }

        [Route("Profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Access", new { area = "" });
            }

            var nhanVien = await db.NhanViens
                .Include(nv => nv.TenDangNhapNavigation)
                .FirstOrDefaultAsync(nv => nv.TenDangNhap == username);

            return View(nhanVien);
        }

        [Route("UpdateProfile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(NhanVien model, IFormFile? imageFile)
        {
            // Lấy nhân viên hiện tại từ database
            var nhanVien = await db.NhanViens.FindAsync(model.MaNhanVien);
            if (nhanVien == null)
            {
                return NotFound();
            }

            // Chỉ cập nhật các trường được phép thay đổi
            // Giữ nguyên các giá trị khác từ database
            if (!string.IsNullOrEmpty(model.TenNhanVien))
                nhanVien.TenNhanVien = model.TenNhanVien.Trim();
            
            if (model.NgaySinh.HasValue)
            {
                // Validate ngày sinh
                var minDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-70));
                var maxDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
                if (model.NgaySinh < minDate || model.NgaySinh > maxDate)
                {
                    TempData["ErrorMessage"] = "Ngày sinh không hợp lệ (tuổi phải từ 18-70)!";
                    return RedirectToAction("Profile");
                }
                nhanVien.NgaySinh = model.NgaySinh;
            }

            if (!string.IsNullOrEmpty(model.SoDienThoai))
            {
                // Validate số điện thoại
                var phoneRegex = new Regex(@"^(0)[0-9]{9}$");
                if (!phoneRegex.IsMatch(model.SoDienThoai))
                {
                    TempData["ErrorMessage"] = "Số điện thoại không hợp lệ!";
                    return RedirectToAction("Profile");
                }
                nhanVien.SoDienThoai = model.SoDienThoai.Trim();
            }

            if (!string.IsNullOrEmpty(model.DiaChi))
                nhanVien.DiaChi = model.DiaChi.Trim();

            if (!string.IsNullOrEmpty(model.GhiChu))
                nhanVien.GhiChu = model.GhiChu.Trim();

            // Xử lý upload ảnh nếu có
            if (imageFile != null && imageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(extension))
                {
                    TempData["ErrorMessage"] = "Chỉ chấp nhận file ảnh .jpg, .jpeg hoặc .png!";
                    return RedirectToAction("Profile");
                }

                if (imageFile.Length > 5 * 1024 * 1024)
                {
                    TempData["ErrorMessage"] = "Kích thước ảnh không được vượt quá 5MB!";
                    return RedirectToAction("Profile");
                }

                // Xóa ảnh cũ
                if (!string.IsNullOrEmpty(nhanVien.AnhDaiDien))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Admin", nhanVien.AnhDaiDien);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Upload ảnh mới
                var fileName = Path.GetFileName(imageFile.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Admin", uniqueFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                nhanVien.AnhDaiDien = uniqueFileName;
                HttpContext.Session.SetString("Avatar", $"/Images/Admin/{uniqueFileName}");
            }

            await db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("Profile");
        }

        [Route("CreateAdmin")]
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [Route("CreateAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(CreateAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra trùng tên đăng nhập
                    var existingAccount = await db.TaiKhoans.FirstOrDefaultAsync(x => x.TenDangNhap == model.TenDangNhap);
                    if (existingAccount != null)
                    {
                        ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại!");
                        return View(model);
                    }

                    // Validate tuổi
                    var minDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-70));
                    var maxDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
                    if (model.NgaySinh < minDate || model.NgaySinh > maxDate)
                    {
                        ModelState.AddModelError("NgaySinh", "Tuổi phải từ 18-70!");
                        return View(model);
                    }

                    // Xử lý upload ảnh
                    string? anhDaiDien = null;
                    if (model.ImageFile != null)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var extension = Path.GetExtension(model.ImageFile.FileName).ToLowerInvariant();
                        
                        if (!allowedExtensions.Contains(extension))
                        {
                            ModelState.AddModelError("ImageFile", "Chỉ chấp nhận file ảnh .jpg, .jpeg hoặc .png!");
                            return View(model);
                        }

                        if (model.ImageFile.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ImageFile", "Kích thước ảnh không được vượt quá 5MB!");
                            return View(model);
                        }

                        var uniqueFileName = $"{Guid.NewGuid()}_{model.ImageFile.FileName}";
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Admin", uniqueFileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        anhDaiDien = uniqueFileName;
                    }

                    // Tạo mã nhân viên mới
                    string maNhanVien;
                    do
                    {
                        maNhanVien = "NV" + MyUtil.GenerateRamdomKey();
                    } while (await db.NhanViens.AnyAsync(x => x.MaNhanVien == maNhanVien));

                    // Tạo tài khoản mới
                    var taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = model.TenDangNhap,
                        MatKhau = "123456".ToSHA256Hash("MySaltKey"),
                        LoaiTaiKhoan = "admin"
                    };

                    // Tạo nhân viên mới
                    var nhanVien = new NhanVien
                    {
                        MaNhanVien = maNhanVien,
                        TenNhanVien = model.TenNhanVien,
                        NgaySinh = model.NgaySinh,
                        SoDienThoai = model.SoDienThoai,
                        DiaChi = model.DiaChi,
                        ChucVu = model.ChucVu,
                        GhiChu = model.GhiChu,
                        AnhDaiDien = anhDaiDien,
                        TenDangNhap = model.TenDangNhap
                    };

                    // Lưu vào database
                    db.TaiKhoans.Add(taiKhoan);
                    db.NhanViens.Add(nhanVien);
                    await db.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Tạo tài khoản admin mới thành công!";
                    return RedirectToAction("DashBoard");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi tạo tài khoản: " + ex.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        [Route("ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin người dùng từ Session
                string userId = HttpContext.Session.GetString("MaNhanVien");
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "Không thể xác định người dùng hiện tại. Vui lòng đăng nhập lại.";
                    return RedirectToAction("Login", "Account");
                }

                // Truy vấn tài khoản người dùng
                var user = await (from tk in db.TaiKhoans
                                  join nv in db.NhanViens on tk.TenDangNhap equals nv.TenDangNhap
                                  where nv.MaNhanVien == userId
                                  select tk).FirstOrDefaultAsync();

                if (user != null)
                {
                    // Kiểm tra mật khẩu hiện tại
                    string hashedCurrentPassword = model.CurrentPassword.ToSHA256Hash("MySaltKey");
                    if (user.MatKhau == hashedCurrentPassword)
                    {
                        // Hash và cập nhật mật khẩu mới
                        user.MatKhau = model.NewPassword.ToSHA256Hash("MySaltKey");
                        await db.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không chính xác.");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy tài khoản người dùng.";
                }
            }

            return View(model);
        }
    }

}


