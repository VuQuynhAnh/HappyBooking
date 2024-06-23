using DemoBuildCoreBlazor.ApiService.Interface;
using DemoBuildCoreShare.Request.User;
using DemoBuildCoreShare.Response.User;

namespace DemoBuildCoreBlazor.ApiService;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> Login(string username, string password)
    {
        var loginModel = new LoginRequest()
        {
            UserName = username,
            Password = password
        };
        var response = await _httpClient.PostAsJsonAsync("api/User/Login", loginModel);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        return null;
    }

    public async Task<string?> RefreshToken(string jwtToken, string refreshToken)
    {
        var refreshModel = new RefreshTokenRequest(jwtToken, refreshToken);
        var response = await _httpClient.PostAsJsonAsync("api/User/RefreshToken", refreshModel);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        return null;
    }
}
