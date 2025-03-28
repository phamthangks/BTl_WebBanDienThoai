using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace BTLW_BDT.APIS
{
    public class EmailValidator
    {
        private static readonly string apiKey = "5a4fe8be9f9e4c3cb74e10c9a10f18ad"; // API Key của bạn
        private static readonly string apiUrl = "https://emailvalidation.abstractapi.com/v1/";

        // Phương thức kiểm tra tính hợp lệ của email qua API
        public static bool ValidateEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Tạo URL với email và API key
                    string url = $"{apiUrl}?api_key={apiKey}&email={email}";
                    HttpResponseMessage response = client.GetAsync(url).Result; // Đồng bộ hóa việc gọi API

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result; // Đọc dữ liệu đồng bộ

                        // Phân tích JSON phản hồi
                        JObject json = JObject.Parse(jsonResponse);

                        // Kiểm tra xem email có hợp lệ hay không
                        bool isValidFormat = json["is_valid_format"]["value"].ToObject<bool>();
                        bool isDeliverable = json["deliverability"].ToString() == "DELIVERABLE";

                        return isValidFormat && isDeliverable; // Email hợp lệ nếu cả hai điều kiện đều đúng
                    }
                    else
                    {
                        Console.WriteLine("API request failed with status code: " + response.StatusCode);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
