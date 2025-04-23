using System.ComponentModel.DataAnnotations;

namespace WebBanDienThoai.API.ViewModels
{
    public class CheckCodeVM
    {
        [Required(ErrorMessage = "Vui lòng nhập mã xác nhận")]
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
