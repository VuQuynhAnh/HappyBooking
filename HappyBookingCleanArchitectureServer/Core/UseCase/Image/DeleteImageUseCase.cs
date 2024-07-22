using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;
using HappyBookingShare.Common;
using HappyBookingShare.Response.ImageUpload;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Image;

public class DeleteImageUseCase: IDeleteImageUseCase
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly IImageRepository _imageRepository;
    private readonly IMemoryCache _cache;

    public DeleteImageUseCase(HttpClient httpClient, IConfiguration configuration, IImageRepository imageRepository, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _clientId = configuration["Imgur:ClientId"] ?? throw new ArgumentNullException(nameof(configuration), "Imgur ClientId is not configured");
        _imageRepository = imageRepository;
        _cache = cache;
    }

    /// <summary>
    /// DeleteImage
    /// </summary>
    /// <param name="deleteHash"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="ApplicationException"></exception>
    public async Task<DeleteImageResponse> DeleteImage(string deleteHash, long userId)
    {
        try
        {
            StatusEnum status = StatusEnum.Successed;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);

            var response = await _httpClient.DeleteAsync($"https://api.imgur.com/3/image/{deleteHash}");

            if (response.IsSuccessStatusCode)
            {
                await _imageRepository.DeleteImage(deleteHash, userId);
                return new DeleteImageResponse(userId, true, status, _cache);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete image from Imgur. Status code: {response.StatusCode}. Response: {responseBody}");
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error deleting image from Imgur", ex);
        }
        finally
        {
            await _imageRepository.ReleaseResource();
        }
    }
}
