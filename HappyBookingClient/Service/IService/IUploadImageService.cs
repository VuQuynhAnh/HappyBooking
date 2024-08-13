using HappyBookingShare.Response.ImageUpload;

namespace HappyBookingClient.Service.IService;

public interface IUploadImageService : IBaseApiService
{
    Task<UploadImageResponse?> UploadImageAsync(IFormFile image);

    Task<UploadImageResponse?> UploadImageWithoutAuthorizeAsync(IFormFile image);

    Task<DeleteImageResponse?> DeleteImageAsync(string deleteHash);

    Task<DeleteImageResponse?> ClearImageNotUsed();
}
