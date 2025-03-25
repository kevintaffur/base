using System.Text;
using front.Dtos.Auth;
using front.Utils.Responses;
using Newtonsoft.Json;

namespace front.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("ApiBase");
        }

        public async Task<string> Login(LoginDto login)
        {
            string json = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_apiKey}/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string c = await response.Content.ReadAsStringAsync();
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(c);
            return loginResponse.Content;
        }

        public async Task<bool> Signup(SignUpDto signup)
        {
            string json = JsonConvert.SerializeObject(signup);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_apiKey}/auth/signup", content);
            return response.IsSuccessStatusCode;
        }
    }
}
