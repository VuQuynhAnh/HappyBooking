using DemoBuildCoreProject.Business.IService;
using DemoBuildCoreProject.Interface;
using HappyBookingShare.Common;
using HappyBookingShare.Response.ImageUpload;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace DemoBuildCoreProject.Business;

public class UploadImageService : IUploadImageService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly IImageRepository _imageRepository;

    public UploadImageService(HttpClient httpClient, IConfiguration configuration, IImageRepository imageRepository)
    {
        _httpClient = httpClient;
        _clientId = configuration["Imgur:ClientId"] ?? throw new ArgumentNullException(nameof(configuration), "Imgur ClientId is not configured");
        _imageRepository = imageRepository;
    }

    public async Task<UploadImageResponse> UploadImageAsync(IFormFile image, long userId)
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
                    return new UploadImageResponse(imageLink, status);
                }
            }
        }
        finally
        {
            await _imageRepository.ReleaseResource();
        }
    }

    public async Task<DeleteImageResponse> DeleteImageAsync(string deleteHash, long userId)
    {
        try
        {
            StatusEnum status = StatusEnum.Successed;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);

            var response = await _httpClient.DeleteAsync($"https://api.imgur.com/3/image/{deleteHash}");

            if (response.IsSuccessStatusCode)
            {
                await _imageRepository.DeleteImage(deleteHash, userId);
                return new DeleteImageResponse(true, status); // Xóa thành công
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

    public async Task<DeleteImageResponse> ClearImageNotUsed()
    {
        try
        {
            var deletedList = await _imageRepository.GetClearImageList();
            StatusEnum status = StatusEnum.Successed;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);
            var tasks = deletedList.Select(image => _httpClient.DeleteAsync($"https://api.imgur.com/3/image/{image}"));
            await Task.WhenAll(tasks);
            return new DeleteImageResponse(true, status);
        }
        finally
        {
            await _imageRepository.ReleaseResource();
        }
    }
}
