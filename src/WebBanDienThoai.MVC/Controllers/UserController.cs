using BTLW_BDT.Helpers;
using BTLW_BDT.Models;
using BTLW_BDT.Models.Api;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace BTLW_BDT.Controllers
{
    public class UserController : Controller
    {
        private readonly BtlLtwQlbdtContext _context;

        public UserController(BtlLtwQlbdtContext context)
        {
            _context = context;
        }

        // Action hiển thị trang Profile
        [HttpGet]
        public IActionResult Profile()
        {
            var khachHang = new KhachHang
            {
                TenKhachHang = HttpContext.Session.GetString("HoTen"),
                //MaKhachHang = HttpContext.Session.GetString("MaKhachHang"),
                //AnhDaiDien = HttpContext.Session.GetString("MaKhachHang"),
                SoDienThoai = HttpContext.Session.GetString("SoDienThoai"),
                DiaChi = HttpContext.Session.GetString("DiaChi"),
                Email = HttpContext.Session.GetString("Email"),
                GhiChu = HttpContext.Session.GetString("GhiChu")
            };

            var ngaySinhStr = HttpContext.Session.GetString("NgaySinh");
            if (DateOnly.TryParse(ngaySinhStr, out var ngaySinh))
                khachHang.NgaySinh = ngaySinh;

            var avatar = HttpContext.Session.GetString("Avatar");
            if (!string.IsNullOrEmpty(avatar))
                khachHang.AnhDaiDien = avatar;

            return View(khachHang);
        }


        // Action lưu thông tin chỉnh sửa của khách hàng
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(KhachHang khachHang, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View("Profile", khachHang);

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(khachHang.MaKhachHang.ToString()), "MaKhachHang");
            content.Add(new StringContent(khachHang.TenKhachHang), "TenKhachHang");
            content.Add(new StringContent(khachHang.NgaySinh.ToString()), "NgaySinh");
            content.Add(new StringContent(khachHang.SoDienThoai), "SoDienThoai");
            content.Add(new StringContent(khachHang.DiaChi), "DiaChi");
            //content.Add(new StringContent(khachHang.AnhDaiDien), "AnhDaiDien");
            content.Add(new StringContent(khachHang.Email), "Email");
            content.Add(new StringContent(khachHang.GhiChu ?? ""), "GhiChu");

            if (imageFile != null)
            {
                var stream = imageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                content.Add(fileContent, "imageFile", imageFile.FileName);
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7145/");

            var response = await client.PostAsync("api/UserAPI/update-profile", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiProfileResult>();
                var data = result?.Data;

                if (data != null)
                {
                    HttpContext.Session.SetString("HoTen", data.TenKhachHang);
                    //HttpContext.Session.SetString("MaKhachHang", data.MaKhachHang);
                    HttpContext.Session.SetString("NgaySinh", data.NgaySinh.ToString());
                    HttpContext.Session.SetString("SoDienThoai", data.SoDienThoai);
                    HttpContext.Session.SetString("DiaChi", data.DiaChi);
                    HttpContext.Session.SetString("Email", data.Email);
                    HttpContext.Session.SetString("GhiChu", data.GhiChu ?? "");

                    if (!string.IsNullOrEmpty(data.AnhDaiDien))
                        HttpContext.Session.SetString("Avatar", Url.Content("~/Images/Customer/" + data.AnhDaiDien));
                }

                TempData["SuccessMessage"] = result?.Message ?? "Cập nhật thành công!";
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                TempData["ErrorMessage"] = error?.Message ?? "Có lỗi xảy ra!";
            }

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // Action xử lý đổi mật khẩu
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string userId = HttpContext.Session.GetString("MaKhachHang");

        //        var user = await (from tk in _context.TaiKhoans
        //                          join kh in _context.KhachHangs on tk.TenDangNhap equals kh.TenDangNhap
        //                          where kh.MaKhachHang == userId
        //                          select tk).FirstOrDefaultAsync();


        //        if (user != null)
        //        {
        //            // Hash mật khẩu hiện tại và so sánh
        //            string hashedCurrentPassword = model.CurrentPassword.ToSHA256Hash("MySaltKey");
        //            if (user.MatKhau == hashedCurrentPassword)
        //            {
        //                // Cập nhật mật khẩu mới sau khi hash
        //                user.MatKhau = model.NewPassword.ToSHA256Hash("MySaltKey");
        //                await _context.SaveChangesAsync();

        //                // Thêm thông báo thành công vào TempData
        //                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";

        //                return RedirectToAction("Profile");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("CurrentPassword", "Không tìm thấy tài khoản người dùng.");
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string? maKhachHang = HttpContext.Session.GetString("MaKhachHang");

            if (string.IsNullOrEmpty(maKhachHang))
            {
                ModelState.AddModelError("", "Không xác định được người dùng.");
                return View(model);
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7145/"); 

                var payload = new
                {
                    MaKhachHang = maKhachHang,
                    CurrentPassword = model.CurrentPassword,
                    NewPassword = model.NewPassword
                };

                var response = await httpClient.PostAsJsonAsync("api/UserAPI/ChangePassword", payload);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                    return RedirectToAction("Profile");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Lỗi đổi mật khẩu: {error}");
                }
            }

            return View(model);
        }
    }

}
