using BTLW_BDT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebBanDienThoai.API.Helpers;
using WebBanDienThoai.API.Models.Api;
using WebBanDienThoai.API.ViewModels;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly BtlLtwQlbdtContext _context;

        public UserAPIController(BtlLtwQlbdtContext context)
        {
            _context = context;
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] KhachHang khachHang, IFormFile? imageFile)
        {
            var existingCustomer = await _context.KhachHangs.FindAsync(khachHang.MaKhachHang);
            if (existingCustomer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng." });

            // Validate số điện thoại
            var phoneRegex = new Regex(@"^(0)[0-9]{9}$");
            if (!phoneRegex.IsMatch(khachHang.SoDienThoai))
                return BadRequest(new { message = "Số điện thoại không hợp lệ!" });

            // Validate email
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!emailRegex.IsMatch(khachHang.Email))
                return BadRequest(new { message = "Email không hợp lệ!" });

            // Cập nhật thông tin
            existingCustomer.TenKhachHang = khachHang.TenKhachHang;
            existingCustomer.NgaySinh = khachHang.NgaySinh;
            existingCustomer.SoDienThoai = khachHang.SoDienThoai;
            existingCustomer.DiaChi = khachHang.DiaChi;
            existingCustomer.Email = khachHang.Email;
            existingCustomer.GhiChu = khachHang.GhiChu;

            // Xử lý ảnh đại diện
            if (imageFile != null && imageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                    return BadRequest(new { message = "Chỉ chấp nhận file ảnh .jpg, .jpeg hoặc .png!" });

                if (imageFile.Length > 5 * 1024 * 1024)
                    return BadRequest(new { message = "Kích thước ảnh không được vượt quá 5MB!" });

                string newAvatarPath = MyUtil.UploadHinh(imageFile, "Customer");
                existingCustomer.AnhDaiDien = newAvatarPath;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Cập nhật thông tin thành công!",
                data = new
                {
                    existingCustomer.TenKhachHang,
                    existingCustomer.NgaySinh,
                    existingCustomer.SoDienThoai,
                    existingCustomer.DiaChi,
                    existingCustomer.Email,
                    existingCustomer.GhiChu,
                    existingCustomer.AnhDaiDien
                }
            });
        }



        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ.");

            var user = await (from tk in _context.TaiKhoans
                              join kh in _context.KhachHangs on tk.TenDangNhap equals kh.TenDangNhap
                              where kh.MaKhachHang == model.MaKhachHang
                              select tk).FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Không tìm thấy tài khoản người dùng.");

            // So sánh mật khẩu hiện tại
            string hashedCurrentPassword = model.CurrentPassword.ToSHA256Hash("MySaltKey");
            if (user.MatKhau != hashedCurrentPassword)
                return BadRequest("Mật khẩu hiện tại không đúng.");

            // Cập nhật mật khẩu mới
            user.MatKhau = model.NewPassword.ToSHA256Hash("MySaltKey");
            await _context.SaveChangesAsync();

            return Ok("Đổi mật khẩu thành công.");
        }

    }
}
