using System.Text;

namespace WebBanDienThoai.API.Helpers
{
    public class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                if (Hinh == null || Hinh.Length == 0)
                    return string.Empty;

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Hinh.FileName);

                string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                string fullPath = Path.Combine(solutionDirectory, "WebBanDienThoai.MVC", "wwwroot", "Images", folder, uniqueFileName);

                string directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    Hinh.CopyTo(stream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here: ex.Message
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
