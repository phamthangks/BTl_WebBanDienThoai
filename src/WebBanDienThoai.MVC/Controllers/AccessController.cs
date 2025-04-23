using BTLW_BDT.Helpers;
using BTLW_BDT.Models;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Login(TaiKhoan model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7145/");

                var response = await client.PostAsJsonAsync("api/AccessAPI/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<JsonElement>();

                    string role = json.GetProperty("role").GetString() ?? "";

                    HttpContext.Session.SetString("Username", json.GetProperty("tenDangNhap").GetString() ?? "");
                    HttpContext.Session.SetString("NgaySinh", json.GetProperty("ngaySinh").GetDateTime().ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("SoDienThoai", json.GetProperty("soDienThoai").GetString() ?? "");
                    HttpContext.Session.SetString("DiaChi", json.GetProperty("diaChi").GetString() ?? "");
                    HttpContext.Session.SetString("GhiChu", json.GetProperty("ghiChu").GetString() ?? "");
                    HttpContext.Session.SetString("AnhDaiDien", json.GetProperty("anhDaiDien").GetString() ?? "");
                    HttpContext.Session.SetString("Role", role);
                    HttpContext.Session.SetString("AvatarUrl", json.GetProperty("avatarUrl").GetString() ?? "");
                    HttpContext.Session.SetString("Email", json.GetProperty("email").GetString() ?? "");
                    if (role == "Customer")
                    {
                        HttpContext.Session.SetString("MaKhachHang", json.GetProperty("maKhachHang").GetString() ?? "");
                        HttpContext.Session.SetString("HoTen", json.GetProperty("tenKhachHang").GetString() ?? "");
                        return RedirectToAction("Index", "Home");
                    }
                    else if (role == "Admin")
                    {
                        HttpContext.Session.SetString("MaNhanVien", json.GetProperty("maNhanVien").GetString() ?? "");
                        HttpContext.Session.SetString("TenNhanVien", json.GetProperty("tenNhanVien").GetString() ?? "");
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        // Trường hợp role không hợp lệ
                        ViewBag.ErrorMessage = "Loại tài khoản không hợp lệ.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return View();
                }
            }
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
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7145/");

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(model.TaiKhoan ?? ""), "TaiKhoan");
                    content.Add(new StringContent(model.MatKhau ?? ""), "MatKhau");
                    content.Add(new StringContent(model.HoTen ?? ""), "HoTen");
                    content.Add(new StringContent(model.NgaySinh.ToString()), "NgaySinh");
                    content.Add(new StringContent(model.DienThoai ?? ""), "DienThoai");
                    content.Add(new StringContent(model.DiaChi ?? ""), "DiaChi");
                    content.Add(new StringContent(model.Email ?? ""), "Email");
                    content.Add(new StringContent(model.DiaChiLatitude?.ToString() ?? "0"), "DiaChiLatitude");
                    content.Add(new StringContent(model.DiaChiLongitude?.ToString() ?? "0"), "DiaChiLongitude");
                    content.Add(new StringContent(model.XacNhanMatKhau ?? ""), "XacNhanMatKhau");
                    var response = await client.PostAsync("api/AccessAPI/register", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadFromJsonAsync<JsonElement>();

                        HttpContext.Session.SetString("MaKhachHang", responseBody.GetProperty("maKhachHang").GetString() ?? "");
                        HttpContext.Session.SetString("HoTen", responseBody.GetProperty("hoTen").GetString() ?? "");
                        HttpContext.Session.SetString("NgaySinh", responseBody.GetProperty("ngaySinh").GetString() ?? "");
                        HttpContext.Session.SetString("SoDienThoai", responseBody.GetProperty("dienThoai").GetString() ?? "");
                        HttpContext.Session.SetString("DiaChi", responseBody.GetProperty("diaChi").GetString() ?? "");
                        HttpContext.Session.SetString("Email", responseBody.GetProperty("email").GetString() ?? "");
                        HttpContext.Session.SetString("Role", responseBody.GetProperty("role").GetString() ?? "");


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = "Lỗi đăng ký: " + error;
                        return View(model);
                    }
                }
            }
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
