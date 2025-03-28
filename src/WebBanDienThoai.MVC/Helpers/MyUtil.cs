using System.Text;

namespace BTLW_BDT.Helpers
{
    public class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                // Tạo tên file duy nhất bằng cách sử dụng Guid kết hợp với tên file gốc
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Hinh.FileName;

                // Đường dẫn lưu file
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folder, uniqueFileName);

                // Tạo thư mục nếu chưa tồn tại
                var directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Lưu file vào thư mục
                using (var myfile = new FileStream(fullPath, FileMode.Create))
                {
                    Hinh.CopyTo(myfile);
                }

                return uniqueFileName; // Trả về tên file duy nhất
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần, ghi log hoặc thông báo lỗi
                return string.Empty;
            }
        }

        public static string GenerateRamdomKey(int length = 5)
        {
            var pattern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRFVTGBYHNUJMIKLOP!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
    }
}
