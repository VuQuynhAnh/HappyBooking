using HappyBookingShare.Response.ImageUpload;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;

public interface IClearImageNotUsedUseCase
{
    Task<DeleteImageResponse> ClearImageNotUsed(long userId);
}
