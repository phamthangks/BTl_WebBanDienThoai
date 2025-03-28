using System.ComponentModel.DataAnnotations;

namespace BTLW_BDT.ViewModels;

public class CreateAdminViewModel
{
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Tên đăng nhập chỉ được chứa chữ cái và số")]
    [StringLength(20, ErrorMessage = "Tên đăng nhập không được vượt quá 20 ký tự")]
    public string TenDangNhap { get; set; } = null!;

    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
    public string TenNhanVien { get; set; } = null!;

    [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
    public DateOnly NgaySinh { get; set; }

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [RegularExpression(@"^(0)[0-9]{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string SoDienThoai { get; set; } = null!;

    [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
    public string DiaChi { get; set; } = null!;

    [Required(ErrorMessage = "Chức vụ là bắt buộc")]
    public string ChucVu { get; set; } = null!;

    public string? GhiChu { get; set; }

    public IFormFile? ImageFile { get; set; }
}
