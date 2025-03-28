using System;
using System.ComponentModel.DataAnnotations;

namespace BTLW_BDT.Models
{
    // Model cho việc cập nhật review
    public class ReviewUpdateModel
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string MaSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn số sao đánh giá")]
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Nội dung đánh giá không được để trống")]
        [StringLength(500, ErrorMessage = "Nội dung đánh giá không được vượt quá 500 ký tự")]
        public string NoiDung { get; set; }
    }

    // Model cho việc tạo review mới
    public class ReviewCreateModel
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string MaSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn số sao đánh giá")]
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Nội dung đánh giá không được để trống")]
        [StringLength(500, ErrorMessage = "Nội dung đánh giá không được vượt quá 500 ký tự")]
        public string NoiDung { get; set; }
    }

    // Model cho việc hiển thị review
    public class ReviewViewModel
    {
        public string TenDangNhap { get; set; }
        public string MaSanPham { get; set; }
        public int Rate { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianDanhGia { get; set; }
        public bool IsCurrentUser { get; set; }
    }
} 