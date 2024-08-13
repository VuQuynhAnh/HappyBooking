using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;
using HappyBookingShare.Common;
using Microsoft.AspNetCore.Components;
using HappyBookingShare.Response.Auth;
using HappyBookingShare.Request.Auth;

namespace HappyBookingClient.Service;

public abstract class BaseApiService
{
    protected readonly HttpClient _httpClient;
    protected readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;

    public BaseApiService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _navigationManager = navigationManager;
    }

    /// <summary>
    /// Send authorized request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected async Task<T?> SendAuthorizedRequestAsync<T>(HttpMethod method, string requestUri, object? content = null)
    {
        var accessToken = await GetTokenFromLocalStorageAsync();
        if (string.IsNullOrEmpty(accessToken))
        {
            var returnUrl = _navigationManager.Uri;
            if (!returnUrl.Contains("register-user"))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                return default;
            }
        }

        var httpRequest = new HttpRequestMessage(method, requestUri);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        if (content != null)
        {
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(httpRequest);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await RefreshToken();
            accessToken = await GetTokenFromLocalStorageAsync();

            var newRequest = new HttpRequestMessage(method, requestUri);
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            if (content != null)
            {
                newRequest.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            }

            response = await _httpClient.SendAsync(newRequest);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var returnUrl = _navigationManager.Uri;
                if (!returnUrl.Contains("register-user"))
                {
                    _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                    return default;
                }
            }
        }

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseBody))
            {
                var result = JsonSerializer.Deserialize<T>(responseBody);
                return result;
            }
        }
        return default;
    }

    /// <summary>
    /// SendMultipartFormDataRequestAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <param name="requestUri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    protected async Task<T?> SendMultipartFormDataRequestAsync<T>(HttpMethod method, string requestUri, MultipartFormDataContent content, bool isAuthorize = true)
    {
        async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshToken();
                request = new HttpRequestMessage(method, requestUri)
                {
                    Headers = { Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenFromLocalStorageAsync()) },
                    Content = content
                };
                response = await _httpClient.SendAsync(request);
            }
            return response;
        }

        var accessToken = await GetTokenFromLocalStorageAsync();
        if (string.IsNullOrEmpty(accessToken) && isAuthorize)
        {
            var returnUrl = _navigationManager.Uri;
            if (!returnUrl.Contains("register-user"))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                return default;
            }
        }

        var request = new HttpRequestMessage(method, requestUri)
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessToken) },
            Content = content
        };

        var finalResponse = await SendRequestAsync(request);

        if (finalResponse.IsSuccessStatusCode)
        {
            var responseBody = await finalResponse.Content.ReadAsStringAsync();
            return !string.IsNullOrEmpty(responseBody) ? JsonSerializer.Deserialize<T>(responseBody) : default;
        }
        return default;
    }

    /// <summary>
    /// Set Token in local storage
    /// </summary>
    /// <param name="token"></param>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    protected async Task SetTokenInLocalStorageAsync(string token, string refreshToken)
    {
        await _localStorage.SetItemAsync(KeyConstant.AuthToken, token);
        await _localStorage.SetItemAsync(KeyConstant.RefreshToken, refreshToken);
    }

    /// <summary>
    /// get token from local storage
    /// </summary>
    /// <returns></returns>
    protected async Task<string> GetTokenFromLocalStorageAsync()
    {
        var result = await _localStorage.GetItemAsync<string>(KeyConstant.AuthToken) ?? string.Empty;
        return result;
    }

    #region private function
    /// <summary>
    /// refresh token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    private async Task<LoginResponse?> RefreshToken()
    {
        var oldRefreshToken = await GetRefreshTokenFromLocalStorageAsync();
        var oldAccessToken = await GetTokenFromLocalStorageAsync();
        if (string.IsNullOrEmpty(oldRefreshToken) || string.IsNullOrEmpty(oldAccessToken))
        {
            return new LoginResponse(StatusEnum.RefreshTokenIsNotExist);
        }

        try
        {
            var request = new RefreshTokenRequest(oldAccessToken, oldRefreshToken);
            var response = await _httpClient.PostAsJsonAsync($"Auth/{APIName.RefreshToken}", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            var newAccessToken = result?.Token ?? string.Empty;
            var newRefreshToken = result?.RefeshToken ?? string.Empty;
            if (!string.IsNullOrEmpty(newAccessToken) && !string.IsNullOrEmpty(newRefreshToken))
            {
                await SetTokenInLocalStorageAsync(newAccessToken, newRefreshToken);
            }
            var returnUrl = _navigationManager.Uri;
            if (!returnUrl.Contains("register-user"))
            {
                _navigationManager.NavigateTo($"/login?returnUrl={Uri.EscapeDataString(returnUrl)}");
                return default;
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// get refresh token
    /// </summary>
    /// <returns></returns>
    private async Task<string> GetRefreshTokenFromLocalStorageAsync()
    {
        var result = await _localStorage.GetItemAsync<string>(KeyConstant.RefreshToken) ?? string.Empty;
        return result;
    }
    #endregion
}
