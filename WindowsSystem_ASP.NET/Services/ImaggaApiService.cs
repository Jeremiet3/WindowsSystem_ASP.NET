using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WindowsSystem_ASP.NET.BL;

namespace WindowsSystem_ASP.NET.Services
{
    public class ImaggaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "acc_0e4e69fd9a3fd0b"; 
        private readonly string _apiSecret = "9628cbc481e7451116a82864faf35b37"; 
        private readonly string _baseUrl = "https://api.imagga.com/v2";

        public ImaggaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{_apiKey}:{_apiSecret}");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<IEnumerable<string>> TagImageAsync(string imageUrl)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/tags?image_url={Uri.EscapeDataString(imageUrl)}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tagsResult = JsonConvert.DeserializeObject<BlTagsResponse>(content);

                // Assurez-vous d'accéder aux tags via `Result`
                var tagNames = tagsResult.Result.Tags
                                          .Where(tag => tag.Confidence > 20)
                                          .Select(tag => tag.Tag.En)
                                          .ToList();
                return tagNames;
            }
            else
            {
                throw new Exception("ERROR");
            }
        }
    }
}
