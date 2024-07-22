using HappyBookingShare.Response.ImageUpload;

namespace HappyBookingServer.Business.IService;

public interface IUploadImageService
{
    Task<UploadImageResponse> UploadImageAsync(IFormFile image, long userId);

    Task<DeleteImageResponse> DeleteImageAsync(string deleteHash, long userId);

    Task<DeleteImageResponse> ClearImageNotUsed(long userId);
}
