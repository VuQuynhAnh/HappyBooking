using Blazored.LocalStorage;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Response.ImageUpload;
using Microsoft.AspNetCore.Components;

namespace HappyBookingClient.Service;

public class UploadImageService : BaseApiService, IUploadImageService
{
    public UploadImageService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager) : base(httpClient, localStorage, navigationManager)
    {
    }

    public async Task<DeleteImageResponse?> ClearImageNotUsed()
    {
        try
        {
            var queryUrl = $"ImageUpload";
            var result = await SendAuthorizedRequestAsync<DeleteImageResponse>(HttpMethod.Delete, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<DeleteImageResponse?> DeleteImageAsync(string deleteHash)
    {
        try
        {
            var queryUrl = $"ImageUpload/{deleteHash}";
            var result = await SendAuthorizedRequestAsync<DeleteImageResponse>(HttpMethod.Delete, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<UploadImageResponse?> UploadImageAsync(IFormFile image)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(image.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
            content.Add(fileContent, "image", image.FileName);

            var result = await SendMultipartFormDataRequestAsync<UploadImageResponse>(HttpMethod.Post, "ImageUpload", content);
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
