using Newtonsoft.Json.Linq;

namespace BTLW_BDT.APIS
{
    public class PhoneValidator
    {
        private static readonly string apiKey = "dfb4a1cdae114d9183c86e5aa50b2df2"; // API Key của bạn
        private static readonly string apiUrl = "https://phonevalidation.abstractapi.com/v1/";

        // Phương thức kiểm tra tính hợp lệ của số điện thoại qua API (Sử dụng đồng bộ)
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Tạo URL với số điện thoại và API key
                    string url = $"{apiUrl}?api_key={apiKey}&phone={phoneNumber}&country=VN";

                    // Gửi yêu cầu GET và nhận phản hồi đồng bộ
                    HttpResponseMessage response = client.GetAsync(url).Result; // Sử dụng .Result để gọi đồng bộ

                    // Kiểm tra xem yêu cầu có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc nội dung của phản hồi đồng bộ
                        string content = response.Content.ReadAsStringAsync().Result; // Dùng .Result để chờ kết quả

                        // Phân tích nội dung phản hồi JSON
                        JObject jsonResponse = JObject.Parse(content);

                        // Kiểm tra xem số điện thoại có hợp lệ không
                        bool isValid = jsonResponse["valid"]?.Value<bool>() ?? false;

                        return isValid;
                    }
                    else
                    {
                        // Nếu có lỗi trong phản hồi, trả về false
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // In ra thông báo lỗi nếu có ngoại lệ xảy ra
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
