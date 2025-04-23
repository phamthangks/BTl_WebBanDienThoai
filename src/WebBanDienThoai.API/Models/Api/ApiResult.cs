using BTLW_BDT.Models;

namespace WebBanDienThoai.API.Models.Api
{
    public class ApiResult<T>
    {
        public string Message { get; set; }
        public KhachHang Data { get; internal set; }
    }
}
