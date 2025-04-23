using System.ComponentModel.DataAnnotations;

namespace WebBanDienThoai.API.ViewModels
{
    public class RegisterVM
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string TaiKhoan { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string XacNhanMatKhau { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateOnly? NgaySinh { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string DiaChi { get; set; }

        //[Required]
        //[Display(Name = "Tỉnh ID")]
        //public int TinhId { get; set; } // ID của Tỉnh Thành

        //[Display(Name = "Tên Tỉnh Thành")]
        //public string TenTinh { get; set; } // Tên đầy đủ của Tỉnh Thành

        //[Required]
        //[Display(Name = "Quận ID")]
        //public int QuanId { get; set; } // ID của Quận Huyện

        //[Display(Name = "Tên Quận Huyện")]
        //public string TenQuan { get; set; } // Tên đầy đủ của Quận Huyện

        //[Required]
        //[Display(Name = "Phường ID")]
        //public int PhuongId { get; set; } // ID của Phường Xã

        //[Display(Name = "Tên Phường Xã")]
        //public string TenPhuong { get; set; } // Tên đầy đủ của Phường Xã



        [Display(Name = "Điện thoại")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 kí tự")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động Việt Nam")]
        public string DienThoai { get; set; }

        [StringLength(320, ErrorMessage = "Email không được vượt quá 320 ký tự.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }

        //[Display(Name = "Hình")]
        //[DataType(DataType.Upload)]
        //public string? Hinh { get; set; }

        public decimal? DiaChiLatitude { get; set; }
        public decimal? DiaChiLongitude { get; set; }


        // Phương thức kiểm tra email hợp lệ
        //public bool IsEmailValid()
        //{
        //    // Kiểm tra tính hợp lệ của email qua API (phương thức ValidateEmail đã đồng bộ)
        //    return EmailValidator.ValidateEmail(Email);
        //}

        //public bool IsPhoneValid()
        //{

        //    return PhoneValidator.ValidatePhoneNumber(DienThoai);
        //}

    }
}
