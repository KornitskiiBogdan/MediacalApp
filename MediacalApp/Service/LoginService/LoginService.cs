using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediacalApp.Service.LoginService
{
    public interface ILoginService
    {
        Task<AuthentificationResult?> Authenticate(string username, string password);

        Task<DummyUser[]> GetUsers();
    }
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };


        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthentificationResult?> Authenticate(string username, string password)
        {
            var response = await _httpClient.PostAsync("auth/login", JsonContent.Create(new { username, password }));

            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<AuthentificationResult>(content, JsonOptions)
                : null;
        }

        public async Task<DummyUser[]> GetUsers()
        {
            var response = await _httpClient.GetFromJsonAsync<UsersResponse>("users");
            return response is null ? Array.Empty<DummyUser>() : response.Users;
        }
    }
}
