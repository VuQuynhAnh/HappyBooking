using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;
using HappyBookingShare.Common;
using HappyBookingShare.Response.ImageUpload;
using System.Net.Http.Headers;
using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Image;

public class ClearImageNotUsedUseCase : IClearImageNotUsedUseCase
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly IImageRepository _imageRepository;
    private readonly IMemoryCache _cache;

    public ClearImageNotUsedUseCase(HttpClient httpClient, IConfiguration configuration, IImageRepository imageRepository, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _clientId = configuration["Imgur:ClientId"] ?? throw new ArgumentNullException(nameof(configuration), "Imgur ClientId is not configured");
        _imageRepository = imageRepository;
        _cache = cache;
    }

    /// <summary>
    /// ClearImageNotUsed
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DeleteImageResponse> ClearImageNotUsed(long userId)
    {
        try
        {
            var deletedList = await _imageRepository.GetClearImageList();
            StatusEnum status = StatusEnum.Successed;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);
            var tasks = deletedList.Select(image => _httpClient.DeleteAsync($"https://api.imgur.com/3/image/{image}"));
            await Task.WhenAll(tasks);
            return new DeleteImageResponse(userId, true, status, _cache);
        }
        finally
        {
            await _imageRepository.ReleaseResource();
        }
    }
}
