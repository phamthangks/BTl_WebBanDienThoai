using BTLW_BDT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using WebBanDienThoai.API.Helpers;
using WebBanDienThoai.API.ViewModels;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessAPIController : ControllerBase
    {
        private readonly BtlLtwQlbdtContext db;

        public AccessAPIController(BtlLtwQlbdtContext context)
        {
            db = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] TaiKhoan user)
        {
            if (user == null || string.IsNullOrEmpty(user.TenDangNhap) || string.IsNullOrEmpty(user.MatKhau))
            {
                return BadRequest(new { message = "Thiếu thông tin đăng nhập." });
            }

            string hashedPassword = user.MatKhau.ToSHA256Hash("MySaltKey");

            var taiKhoan = db.TaiKhoans.FirstOrDefault(tk =>
                tk.TenDangNhap == user.TenDangNhap &&
                tk.MatKhau == hashedPassword);

            if (taiKhoan == null)
            {
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }

            if (taiKhoan.LoaiTaiKhoan == "customer")
            {
                var userInfo = (from tk in db.TaiKhoans
                                join kh in db.KhachHangs on tk.TenDangNhap equals kh.TenDangNhap
                                where tk.TenDangNhap == user.TenDangNhap
                                select new
                                {
                                    tk.TenDangNhap,
                                    kh.MaKhachHang,
                                    kh.TenKhachHang,
                                    kh.NgaySinh,
                                    kh.SoDienThoai,
                                    kh.DiaChi,
                                    kh.Email,
                                    kh.GhiChu,
                                    kh.AnhDaiDien,
                                    Role = "Customer",
                                    AvatarUrl = Url.Content("../WebBanDienThoai.MVC/wwwroot/Images/Images/Customer/" + kh.AnhDaiDien)
                                }).FirstOrDefault();

                return Ok(userInfo);
            }
            else if (taiKhoan.LoaiTaiKhoan == "admin")
            {
                var adminInfo = (from tk in db.TaiKhoans
                                 join nv in db.NhanViens on tk.TenDangNhap equals nv.TenDangNhap
                                 where tk.TenDangNhap == user.TenDangNhap
                                 select new
                                 {
                                     tk.TenDangNhap,
                                     nv.MaNhanVien,
                                     nv.TenNhanVien,
                                     nv.NgaySinh,
                                     nv.SoDienThoai,
                                     nv.DiaChi,
                                     nv.ChucVu,
                                     nv.GhiChu,
                                     nv.AnhDaiDien,
                                     Role = "Admin",
                                     AvatarUrl = Url.Content("~/Images/Admin/" + nv.AnhDaiDien)
                                 }).FirstOrDefault();

                return Ok(adminInfo);
            }

            return BadRequest(new { message = "Loại tài khoản không hợp lệ." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra tài khoản đã tồn tại
            var existingAccount = await db.TaiKhoans
                .FirstOrDefaultAsync(x => x.TenDangNhap == model.TaiKhoan);

            if (existingAccount != null)
            {
                return Conflict(new { message = "Tên đăng nhập đã tồn tại." });
            }

            // Tạo Khách hàng mới
            var khachHang = new KhachHang
            {
                MaKhachHang = MyUtil.GenerateRamdomKey(),
                TenKhachHang = model.HoTen,
                NgaySinh = model.NgaySinh,
                SoDienThoai = model.DienThoai,
                DiaChi = model.DiaChi,
                Email = model.Email,
                TenDangNhap = model.TaiKhoan,
                DiaChiLatitude = model.DiaChiLatitude,
                DiaChiLongitude = model.DiaChiLongitude
            };

            // Băm mật khẩu
            string hashedPassword = model.MatKhau.ToSHA256Hash("MySaltKey");

            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = model.TaiKhoan,
                MatKhau = hashedPassword,
                LoaiTaiKhoan = "customer"
            };

            // Upload ảnh đại diện nếu có
      
            
                //khachHang.AnhDaiDien = "";
            

            db.KhachHangs.Add(khachHang);
            db.TaiKhoans.Add(taiKhoan);
            await db.SaveChangesAsync();

            // Trả về thông tin người dùng vừa đăng ký
            return Ok(new
            {
                success = true,
                message = "Đăng ký thành công.",
                tenDangNhap = khachHang.TenDangNhap,
                maKhachHang = khachHang.MaKhachHang,
                hoTen = khachHang.TenKhachHang,
                ngaySinh = khachHang.NgaySinh.ToString(),
                dienThoai = khachHang.SoDienThoai,
                diaChi = khachHang.DiaChi,
                email = khachHang.Email,
                role = "Customer"
            });
        }

        [HttpPost("request-reset")]
        public IActionResult RequestReset([FromBody] ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var user = db.KhachHangs.SingleOrDefault(x => x.Email == model.Email);
            if (user != null)
            {
                var code = new Random().Next(100000, 999999);
                user.ResetCode = code;
                user.ResetCodeExpiry = DateTime.Now.AddMinutes(5);
                db.SaveChanges();

                SendResetCodeEmail(user.Email, code);

                return Ok(new { message = "Đã gửi mã xác thực", email = user.Email });
            }

            return NotFound(new { message = "Email không tồn tại." });
        }

        private void SendResetCodeEmail(string email, int code)
        {
            var fromAddress = new MailAddress("thangdepzai38@gmail.com", "Admin");
            var toAddress = new MailAddress(email);
            const string fromPassword = "wxpl wnto nnqf uyyx";
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

        [HttpPost("check-code")]
        public IActionResult CheckCode([FromBody] CheckCodeVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var user = db.KhachHangs.SingleOrDefault(x =>
                x.Email == model.Email &&
                x.ResetCode.ToString() == model.Code);

            if (user != null)
            {
                if (user.ResetCodeExpiry.HasValue && user.ResetCodeExpiry.Value > DateTime.Now)
                {
                    return Ok(new { message = "Mã hợp lệ", email = model.Email });
                }
                else
                {
                    user.ResetCode = null;
                    user.ResetCodeExpiry = null;
                    db.SaveChanges();
                    return BadRequest(new { message = "Mã xác thực đã hết hạn." });
                }
            }

            return BadRequest(new { message = "Mã xác thực không hợp lệ." });
        }

        [HttpPost("new-password")]
        public IActionResult NewPassword([FromBody] NewPasswordVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            var user = db.KhachHangs.SingleOrDefault(x => x.Email == model.Email);
            if (user != null)
            {
                var account = db.TaiKhoans.SingleOrDefault(x => x.TenDangNhap == user.TenDangNhap);
                if (account != null)
                {
                    account.MatKhau = model.NewPassword.ToSHA256Hash("MySaltKey");
                    user.ResetCode = null;
                    user.ResetCodeExpiry = null;
                    db.SaveChanges();

                    return Ok(new { message = "Đặt lại mật khẩu thành công!" });
                }
            }

            return BadRequest(new { message = "Đã xảy ra lỗi. Vui lòng thử lại." });
        }
    }
}
