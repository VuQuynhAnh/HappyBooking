using HappyBookingShare.Response.ImageUpload;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;

public interface IUploadImageUseCase
{
    Task<UploadImageResponse> UploadImage(IFormFile image, long userId);
}
