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
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BTLW_BDT.Models.Api;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
                    HttpContext.Session.SetString("Avatar", json.GetProperty("avatarUrl").GetString() ?? "");
              
                    if (role == "Customer")
                    {
                        HttpContext.Session.SetString("Email", json.GetProperty("email").GetString() ?? "");
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

        public async Task LoginByGoogle()
        {
            // Use Google authentication scheme for challenge
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }
        public async Task<IActionResult> GoogleResponse()
        {

            // Authenticate using Google scheme
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                //Nếu xác thực ko thành công quay về trang Login
                return RedirectToAction("Login");
            }

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            string emailName = email.Split('@')[0];
            //return Json(claims);


            // Check email có tồn tại không
            var existingUser = await db.KhachHangs.FirstOrDefaultAsync(u => u.Email == email);
            // Kiểm tra trùng tên đăng nhập
            var existingAccount = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == emailName);

            if (existingUser == null && existingAccount == null)
            {

                var khachHang = new KhachHang
                {
                    MaKhachHang = MyUtil.GenerateRamdomKey(),
                    TenKhachHang = name,
                    Email = email,
                    TenDangNhap = emailName,
                };

                // //nếu user ko tồn tại trong db thì tạo user mới với password hashed mặc định 1-9
                string passwordDefault = "123456789";
                string hashedPassword = passwordDefault.ToSHA256Hash("MySaltKey");
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = emailName,
                    MatKhau = hashedPassword,
                    LoaiTaiKhoan = "customer"
                };
                // Set session values
                HttpContext.Session.SetString("Username", khachHang.TenDangNhap);
                HttpContext.Session.SetString("HoTen", khachHang.TenKhachHang);
                HttpContext.Session.SetString("Email", khachHang.Email);

                db.KhachHangs.Add(khachHang);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");


            }
            else
            {

                // Xử lý đăng nhập cho khách hàng
                var userInfo = (from tk in db.TaiKhoans
                                join kh in db.KhachHangs on tk.TenDangNhap equals kh.TenDangNhap
                                where tk.TenDangNhap == emailName
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
                    //HttpContext.Session.SetString("NgaySinh", $"{userInfo.NgaySinh}");
                    //HttpContext.Session.SetString("SoDienThoai", userInfo.SoDienThoai);
                    //HttpContext.Session.SetString("DiaChi", userInfo.DiaChi);
                    //HttpContext.Session.SetString("Email", userInfo.Email);
                    //HttpContext.Session.SetString("GhiChu", userInfo.GhiChu ?? "");
                    HttpContext.Session.SetString("Avatar", Url.Content("~/Images/Customer/" + userInfo.AnhDaiDien));
                    HttpContext.Session.SetString("Role", "Customer");

                    return RedirectToAction("Index", "Home");
                }

            }

            return RedirectToAction("Login");

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
                        var error1 = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                        ModelState.AddModelError(string.Empty, error1?.Message ?? "Lỗi đăng ký:.");
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
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7145/");
            var response = await client.PostAsJsonAsync("api/AccessAPI/request-reset", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Email"] = model.Email;
                return RedirectToAction("CheckCode");
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            ModelState.AddModelError(string.Empty, error?.Message ?? "Đã xảy ra lỗi.");
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
        public async Task<IActionResult> CheckCode(CheckCodeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7145/");
            var response = await client.PostAsJsonAsync("api/AccessAPI/check-code", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Email"] = model.Email;
                return RedirectToAction("NewPassword");
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            ModelState.AddModelError(string.Empty, error?.Message ?? "Xác thực thất bại.");

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
        public async Task<IActionResult> NewPassword(NewPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7145/");
            var response = await client.PostAsJsonAsync("api/AccessAPI/new-password", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công!";
                return RedirectToAction("Login");
            }

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            TempData["ErrorMessage"] = error?.Message ?? "Lỗi không xác định.";
            return RedirectToAction("ForgotPassword");
        }

        //private class ApiErrorResponse
        //{
        //    public string? Message { get; internal set; }
        //}

        //private void SendResetCodeEmail(string email, int code)
        //{
        //    var fromAddress = new MailAddress("thangdepzai38@gmail.com", "Admin");
        //    var toAddress = new MailAddress(email);
        //    const string fromPassword = "wxpl wnto nnqf uyyx";
        //    const string subject = "Reset mật khẩu - Mã xác thực";
        //    string body = $"Mã xác thực của bạn là: {code}";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //    };

        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = subject,
        //        Body = body
        //    })
        //    {
        //        smtp.Send(message);
        //    }
        //}

        //private int GenerateResetCode()
        //{
        //    var rng = new Random();
        //    return rng.Next(100000, 999999); // Tạo mã ngẫu nhiên 6 chữ số
        //}

    }
}
