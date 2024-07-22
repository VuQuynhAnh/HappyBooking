using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;
using HappyBookingShare.Common;
using HappyBookingShare.Response.ImageUpload;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Image;

public class UploadImageUseCase : IUploadImageUseCase
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly IImageRepository _imageRepository;
    private readonly IMemoryCache _cache;

    public UploadImageUseCase(HttpClient httpClient, IConfiguration configuration, IImageRepository imageRepository, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _clientId = configuration["Imgur:ClientId"] ?? throw new ArgumentNullException(nameof(configuration), "Imgur ClientId is not configured");
        _imageRepository = imageRepository;
        _cache = cache;
    }

    /// <summary>
    /// UploadImageAsync
    /// </summary>
    /// <param name="image"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<UploadImageResponse> UploadImage(IFormFile image, long userId)
    {
        try
        {
            StatusEnum status = StatusEnum.Successed;
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Image is required", nameof(image));
            }

            using (var content = new MultipartFormDataContent())
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    var imageData = ms.ToArray();
                    content.Add(new ByteArrayContent(imageData), "image", image.FileName);

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);
                    var response = await _httpClient.PostAsync("https://api.imgur.com/3/image", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException($"Error uploading image to Imgur: {responseBody}");
                    }

                    var json = JObject.Parse(await response.Content.ReadAsStringAsync());

                    if (json["success"]?.Value<bool>() != true)
                    {
                        throw new HttpRequestException("Failed to upload image to Imgur.");
                    }

                    var imageLink = json["data"]?["link"]?.Value<string>();
                    if (string.IsNullOrEmpty(imageLink))
                    {
                        throw new HttpRequestException("Failed to retrieve image link from Imgur response.");
                    }
                    await _imageRepository.UploadImage(imageLink, userId);
                    return new UploadImageResponse(userId, imageLink, status, _cache);
                }
            }
        }
        finally
        {
            await _imageRepository.ReleaseResource();
        }
    }
}
