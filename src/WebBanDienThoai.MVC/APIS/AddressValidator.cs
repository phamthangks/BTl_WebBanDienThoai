using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BTLW_BDT.APIS
{
    public class AddressValidator
    {
        private static readonly HttpClient client = new HttpClient();

        // Method to get provinces (Tỉnh Thành)
        public async Task<AddressResponse> GetProvincesAsync()
        {
            var response = await client.GetStringAsync("https://esgoo.net/api-tinhthanh/1/0.htm");
            var data = JsonConvert.DeserializeObject<AddressResponse>(response);
            return data;
        }

        // Method to get districts (Quận Huyện) based on province id
        public async Task<AddressResponse> GetDistrictsAsync(int provinceId)
        {
            var response = await client.GetStringAsync($"https://esgoo.net/api-tinhthanh/2/{provinceId}.htm");
            var data = JsonConvert.DeserializeObject<AddressResponse>(response);
            return data;
        }

        // Method to get wards (Phường Xã) based on district id
        public async Task<AddressResponse> GetWardsAsync(int districtId)
        {
            var response = await client.GetStringAsync($"https://esgoo.net/api-tinhthanh/3/{districtId}.htm");
            var data = JsonConvert.DeserializeObject<AddressResponse>(response);
            return data;
        }
    }

    // Class to represent the response structure
    public class AddressResponse
    {
        public int error { get; set; }
        public List<AddressData> data { get; set; }
    }

    // Class to represent address data (province, district, ward)
    public class AddressData
    {
        public int id { get; set; }
        public string full_name { get; set; }
    }
}
