using BTLW_BDT.Models;
using BTLW_BDT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BTLW_BDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly BtlLtwQlbdtContext _context;

        public ReviewController(BtlLtwQlbdtContext context)
        {
            _context = context;
        }

        private bool HasPurchasedProduct(string username, string productId)
        {
            var customer = _context.KhachHangs.FirstOrDefault(k => k.TenDangNhap == username);
            if (customer == null) return false;


            var hasPurchased = (from hd in _context.HoaDonBans
                               join ct in _context.ChiTietHoaDonBans on hd.MaHoaDon equals ct.MaHoaDon
                               where hd.MaKhachHang == customer.MaKhachHang
                               && ct.MaSanPham == productId
                               select ct).Any();

            
            return hasPurchased;
        }


        [HttpGet("productreview/{productId}")]
        public IActionResult ProductReview(string productId)
        {
            try 

            {
                var username = HttpContext.Session.GetString("Username");
                var canReview = false;
                var hasPurchased = false;

                if (!string.IsNullOrEmpty(username))
                {
                    hasPurchased = HasPurchasedProduct(username, productId);
                    canReview = hasPurchased;
                }

                var reviews = from r in _context.DanhGia
                             where r.MaSanPham == productId
                             orderby r.ThoiGianDanhGia descending
                             select new ViewModels.ReviewViewModel
                             {
                                 TenDangNhap = r.TenDangNhap,
                                 MaSanPham = r.MaSanPham,
                                 Rate = (int)r.Rate,
                                 NoiDung = r.NoiDung,
                                 ThoiGianDanhGia = r.ThoiGianDanhGia,
                                 IsCurrentUser = r.TenDangNhap == username
                             };

                var product = _context.SanPhams.FirstOrDefault(p => p.MaSanPham == productId);

                return Json(new { 
                    reviews = reviews.ToList(), 
                    dmSp = new { 
                        tenSanPham = product.TenSanPham 
                    },
                    canReview = canReview,
                    hasPurchased = hasPurchased,
                    isLoggedIn = !string.IsNullOrEmpty(username)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("CreateReview/{productId}")]
        public IActionResult CreateReview(string productId)
        {
            var username = HttpContext.Session.GetString("Username");
            var existingReview = _context.DanhGia
                .FirstOrDefault(r => r.TenDangNhap == username && r.MaSanPham == productId);

            return PartialView("_ReviewForm", new ViewModels.ReviewFormViewModel 
            { 
                MaSanPham = productId,
                ExistingReview = existingReview
            });
        }

        [HttpPost("submitreview")]
        public IActionResult SubmitReview([FromBody] ViewModels.ReviewCreateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });

                var username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Vui lòng đăng nhập để đánh giá" });

                if (!HasPurchasedProduct(username, model.MaSanPham))
                    return Json(new { success = false, message = "Bạn cần mua sản phẩm để đánh giá" });

                var existingReview = _context.DanhGia
                    .FirstOrDefault(r => r.TenDangNhap == username && r.MaSanPham == model.MaSanPham);

                if (existingReview != null)
                    return Json(new { success = false, message = "Bạn đã đánh giá sản phẩm này" });

                var review = new DanhGium
                {
                    TenDangNhap = username,
                    MaSanPham = model.MaSanPham,
                    Rate = model.Rate,
                    NoiDung = model.NoiDung,
                    ThoiGianDanhGia = DateTime.Now
                };

                _context.DanhGia.Add(review);
                _context.SaveChanges();

                return Json(new { success = true, message = "Đánh giá đã được gửi thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost("updatereview")]
        public IActionResult UpdateReview([FromBody] ViewModels.ReviewUpdateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });

                var username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Vui lòng đăng nhập để sửa đánh giá" });

                var existingReview = _context.DanhGia
                    .FirstOrDefault(r => r.TenDangNhap == username && r.MaSanPham == model.MaSanPham);

                if (existingReview == null)
                    return Json(new { success = false, message = "Không tìm thấy đánh giá" });

                existingReview.Rate = model.Rate;
                existingReview.NoiDung = model.NoiDung;
                existingReview.ThoiGianDanhGia = DateTime.Now;

                _context.SaveChanges();
                return Json(new { success = true, message = "Cập nhật đánh giá thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPost("deletereview/{productId}")]
        public IActionResult DeleteReview(string productId)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return Json(new { success = false, message = "Vui lòng đăng nhập để xóa đánh giá" });

            var review = _context.DanhGia
                .FirstOrDefault(r => r.TenDangNhap == username && r.MaSanPham == productId);

            if (review == null)
                return Json(new { success = false, message = "Không tìm thấy đánh giá" });

            _context.DanhGia.Remove(review);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpGet("GetReviewForEdit/{productId}")]
        public IActionResult GetReviewForEdit(string productId)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Vui lòng đăng nhập để sửa đánh giá" });

                var review = _context.DanhGia
                    .FirstOrDefault(r => r.TenDangNhap == username && r.MaSanPham == productId);

                if (review == null)
                    return Json(new { success = false, message = "Không tìm thấy đánh giá" });

                return Json(new { 
                    success = true, 
                    review = new { 
                        maSanPham = review.MaSanPham,
                        rate = review.Rate,
                        noiDung = review.NoiDung
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}
