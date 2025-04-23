using System.ComponentModel.DataAnnotations;

namespace WebBanDienThoai.API.ViewModels
{
    public class ChangePasswordVM
    {
        public string MaKhachHang { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
