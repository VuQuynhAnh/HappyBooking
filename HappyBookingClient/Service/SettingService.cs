using Blazored.LocalStorage;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
using HappyBookingShare.Response.User;
using Microsoft.AspNetCore.Components;

namespace HappyBookingClient.Service;

public class SettingService : BaseApiService, ISettingService
{
    public SettingService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager) : base(httpClient, localStorage, navigationManager)
    {
    }

    /// <summary>
    /// Get setting
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetSettingResponse?> GetSetting()
    {
        try
        {
            var queryUrl = $"Setting/{APIName.GetSetting}";
            var result = await SendAuthorizedRequestAsync<GetSettingResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<SaveSettingResponse?> SaveSetting(SaveSettingRequest request)
    {
        try
        {
            var queryUrl = $"Setting/{APIName.SaveSetting}";
            var result = await SendAuthorizedRequestAsync<SaveSettingResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// get token from local storage
    /// </summary>
    /// <returns></returns>
    async Task<string> IBaseApiService.GetTokenFromLocalStorageAsync()
    {
        return await GetTokenFromLocalStorageAsync();
    }
}
