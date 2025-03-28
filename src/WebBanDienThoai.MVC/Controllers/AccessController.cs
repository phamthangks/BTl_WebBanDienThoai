using BTLW_BDT.Helpers;
using BTLW_BDT.Models;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net.Mail;
using System.Net;

namespace BTLW_BDT.Controllers
{
    public class AccessController : Controller
    {
        //BtlLtwQlbdtContext db = new BtlLtwQlbdtContext();
        private readonly BtlLtwQlbdtContext db;
   
        public AccessController(BtlLtwQlbdtContext context)
        {
            db = context;

        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                string hashedPassword = user.MatKhau.ToSHA256Hash("MySaltKey");
                
                // Kiểm tra tài khoản tồn tại
                var taiKhoan = db.TaiKhoans.FirstOrDefault(tk => 
                    tk.TenDangNhap == user.TenDangNhap && 
                    tk.MatKhau == hashedPassword);

                if (taiKhoan != null)
                {
                    if (taiKhoan.LoaiTaiKhoan == "customer")
                    {
                        // Xử lý đăng nhập cho khách hàng
                        var userInfo = (from tk in db.TaiKhoans
                                    join kh in db.KhachHangs on tk.TenDangNhap equals kh.TenDangNhap
                                    where tk.TenDangNhap == user.TenDangNhap
                                    select new
                                    {
                                        tk.TenDangNhap,
                                        kh.AnhDaiDien,
                                        kh.MaKhachHang,
                                        kh.TenKhachHang,
                                        kh.NgaySinh,
                                        kh.SoDienThoai,
                                        kh.DiaChi,
                                        kh.Email,
                                        kh.GhiChu
                                    }).FirstOrDefault();

                        if (userInfo != null)
                        {
                            HttpContext.Session.SetString("Username", userInfo.TenDangNhap);
                            HttpContext.Session.SetString("MaKhachHang", userInfo.MaKhachHang);
                            HttpContext.Session.SetString("HoTen", userInfo.TenKhachHang);
                            HttpContext.Session.SetString("NgaySinh", $"{userInfo.NgaySinh}");
                            HttpContext.Session.SetString("SoDienThoai", userInfo.SoDienThoai);
                            HttpContext.Session.SetString("DiaChi", userInfo.DiaChi);
                            HttpContext.Session.SetString("Email", userInfo.Email);
                            HttpContext.Session.SetString("GhiChu", userInfo.GhiChu ?? "");
                            HttpContext.Session.SetString("Avatar", Url.Content("~/Images/Customer/" + userInfo.AnhDaiDien));
                            HttpContext.Session.SetString("Role", "Customer");
                            
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if (taiKhoan.LoaiTaiKhoan == "admin")
                    {
                        // Xử lý đăng nhập cho admin
                        var adminInfo = (from tk in db.TaiKhoans
                                     join nv in db.NhanViens on tk.TenDangNhap equals nv.TenDangNhap
                                     where tk.TenDangNhap == user.TenDangNhap
                                     select new
                                     {
                                         tk.TenDangNhap,
                                         nv.AnhDaiDien,
                                         nv.MaNhanVien,
                                         nv.TenNhanVien,
                                         nv.NgaySinh,
                                         nv.SoDienThoai,
                                         nv.DiaChi,
                                         nv.ChucVu,
                                         nv.GhiChu
                                     }).FirstOrDefault();

                        if (adminInfo != null)
                        {
                            HttpContext.Session.SetString("Username", adminInfo.TenDangNhap);
                            HttpContext.Session.SetString("MaNhanVien", adminInfo.MaNhanVien);
                            HttpContext.Session.SetString("HoTen", adminInfo.TenNhanVien);
                            HttpContext.Session.SetString("NgaySinh", $"{adminInfo.NgaySinh}");
                            HttpContext.Session.SetString("SoDienThoai", adminInfo.SoDienThoai);
                            HttpContext.Session.SetString("DiaChi", adminInfo.DiaChi);
                            HttpContext.Session.SetString("ChucVu", adminInfo.ChucVu);
                            HttpContext.Session.SetString("GhiChu", adminInfo.GhiChu ?? "");
                            HttpContext.Session.SetString("Avatar", Url.Content("~/Images/Admin/" + adminInfo.AnhDaiDien));
                            HttpContext.Session.SetString("Role", "Admin");

                            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                        }
                    }
                }

                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa session
            HttpContext.Session.Clear();
            
            // Xóa cookies nếu có
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            
            // Redirect về trang login
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng tên đăng nhập
                var existingAccount = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == model.TaiKhoan);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("TaiKhoan", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }


                var khachHang = new KhachHang
                {
                    MaKhachHang = MyUtil.GenerateRamdomKey(),
                    TenKhachHang = model.HoTen,
                    NgaySinh = model.NgaySinh,
                    SoDienThoai = model.DienThoai,
                    DiaChi = model.DiaChi,
                    Email = model.Email,
                    TenDangNhap = model.TaiKhoan,
                    // Thêm 2 trường tọa độ
                    DiaChiLatitude = model.DiaChiLatitude,
                    DiaChiLongitude = model.DiaChiLongitude
                };


                string hashedPassword = model.MatKhau.ToSHA256Hash("MySaltKey");
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = model.TaiKhoan,
                    MatKhau = hashedPassword,
                    LoaiTaiKhoan = "customer"
                };


                if (Hinh != null)
                {
                    khachHang.AnhDaiDien = MyUtil.UploadHinh(Hinh, "Customer");

                }

                db.KhachHangs.Add(khachHang);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();

                // Set session values
                HttpContext.Session.SetString("Username", khachHang.TenDangNhap);
                HttpContext.Session.SetString("HoTen", khachHang.TenKhachHang);
                HttpContext.Session.SetString("NgaySinh", khachHang.NgaySinh.ToString());
                HttpContext.Session.SetString("SoDienThoai", khachHang.SoDienThoai);
                HttpContext.Session.SetString("DiaChi", khachHang.DiaChi);
                HttpContext.Session.SetString("Email", khachHang.Email);
                HttpContext.Session.SetString("GhiChu", khachHang.GhiChu ?? "");
                HttpContext.Session.SetString("Avatar", Url.Content("~/Images/Customer/" + khachHang.AnhDaiDien));
                HttpContext.Session.SetString("Role", "Customer");

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = db.KhachHangs.SingleOrDefault(x => x.Email == model.Email);
            if (user != null)
            {
                // Tạo mã xác thực
                var code = GenerateResetCode();

               
               

                // Lưu mã xác thực vào cơ sở dữ liệu
                user.ResetCode = code;
                user.ResetCodeExpiry = DateTime.Now.AddMinutes(5);

                db.SaveChanges();

                // Gửi mã qua email 
                SendResetCodeEmail(model.Email, code);

                // Chuyển email vào TempData để dùng ở bước kiểm tra mã
                TempData["Email"] = model.Email;

                return RedirectToAction("CheckCode");
            }

            ModelState.AddModelError(string.Empty, "Email không tồn tại.");
            return View(model);
        }

        [HttpGet]
        public IActionResult CheckCode()
        {
            var email = TempData["Email"]?.ToString();
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }

            TempData.Keep("Email"); // Giữ lại giá trị cho POST request
            return View(new CheckCodeVM { Email = email });
        }

        [HttpPost]
        public IActionResult CheckCode(CheckCodeVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = db.KhachHangs.SingleOrDefault(x =>
                x.Email == model.Email &&
                x.ResetCode.ToString() == model.Code);

            if (user != null)
            {
                // Kiểm tra thời gian hết hạn
                if (user.ResetCodeExpiry.Value > DateTime.Now)
                {
                    TempData["Email"] = model.Email;
                    return RedirectToAction("NewPassword");
                }
                else
                {
                    // Mã đã hết hạn
                    user.ResetCode = null;
                    user.ResetCodeExpiry = null;
                    db.SaveChanges();

                    ModelState.AddModelError(string.Empty, "Mã xác thực đã hết hạn. Vui lòng yêu cầu mã mới.");
                    return View(model);
                }
            }

            ModelState.AddModelError(string.Empty, "Mã xác thực không hợp lệ");
            return View(model);
        }


        [HttpGet]
        public IActionResult NewPassword()
        {
            var email = TempData["Email"]?.ToString();
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }

            TempData.Keep("Email");
            return View(new NewPasswordVM { Email = email });
        }

        [HttpPost]
        public IActionResult NewPassword(NewPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = db.KhachHangs.SingleOrDefault(x => x.Email == model.Email);
            if (user != null)
            {
                var account = db.TaiKhoans.SingleOrDefault(x => x.TenDangNhap == user.TenDangNhap);
                if (account != null)
                {
                    account.MatKhau = model.NewPassword.ToSHA256Hash("MySaltKey");
                    user.ResetCode = null; // Xóa mã sau khi dùng
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công!";
                    return RedirectToAction("Login");
                }
            }

            TempData["ErrorMessage"] = "Đã xảy ra lỗi. Vui lòng thử lại.";
            return RedirectToAction("ForgotPassword");
        }

        private void SendResetCodeEmail(string email, int code)
        {
            var fromAddress = new MailAddress("thangdepzai38@gmail.com", "Admin");
            var toAddress = new MailAddress(email);
            const string fromPassword = "iflx rhxm wyjf hlbw";
            const string subject = "Reset mật khẩu - Mã xác thực";
            string body = $"Mã xác thực của bạn là: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private int GenerateResetCode()
        {
            var rng = new Random();
            return rng.Next(100000, 999999); // Tạo mã ngẫu nhiên 6 chữ số
        }

    }
}
