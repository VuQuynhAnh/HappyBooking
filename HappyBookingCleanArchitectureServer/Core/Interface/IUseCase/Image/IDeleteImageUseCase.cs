using HappyBookingShare.Response.ImageUpload;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;

public interface IDeleteImageUseCase
{
    Task<DeleteImageResponse> DeleteImage(string deleteHash, long userId);
}
