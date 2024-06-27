using Blazored.LocalStorage;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;
using Microsoft.AspNetCore.Components;

namespace HappyBookingClient.Service;

public class UserService : BaseApiService, IUserService
{
    public UserService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager) : base(httpClient, localStorage, navigationManager)
    {
    }

    /// <summary>
    /// get all user data
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetListUserResponse?> GetAllUserData(GetListUserRequest request)
    {
        try
        {
            string keyword = string.IsNullOrEmpty(request.KeyWord) ? "_" : request.KeyWord;
            var queryUrl = $"User/{APIName.GetAllData}?PageIndex={request.PageIndex}&PageSize={request.PageSize}&KeyWord={keyword}";
            var result = await SendAuthorizedRequestAsync<GetListUserResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User/Login", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            var token = result?.Token ?? string.Empty;
            var refreshToken = result?.RefeshToken ?? string.Empty;
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken))
            {
                await SetTokenInLocalStorageAsync(token, refreshToken);
                return result;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// register new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse?> RegisterUser(RegisterUserRequest request)
    {
        try
        {
            var queryUrl = $"User/{APIName.RegisterUser}";
            var result = await SendAuthorizedRequestAsync<SaveUserResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// update user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<SaveUserResponse?> UpdateUser(UpdateUserRequest request)
    {
        try
        {
            var queryUrl = $"User/{APIName.UpdateUser}";
            var result = await SendAuthorizedRequestAsync<SaveUserResponse>(HttpMethod.Put, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// remove tokens from local storage
    /// </summary>
    /// <returns></returns>
    public async Task RemoveTokensFromLocalStorageAsync()
    {
        await _localStorage.RemoveItemAsync(KeyConstant.AuthToken);
        await _localStorage.RemoveItemAsync(KeyConstant.RefreshToken);
    }

    /// <summary>
    /// get token from local storage
    /// </summary>
    /// <returns></returns>
    async Task<string> IBaseApiService.GetTokenFromLocalStorageAsync()
    {
        return await base.GetTokenFromLocalStorageAsync();
    }
}
