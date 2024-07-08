using Blazored.LocalStorage;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
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
            var queryUrl = $"Setting";
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
        throw new NotImplementedException();
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
