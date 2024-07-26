using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IChangePasswordUseCase
{
    Task<SaveUserResponse> ChangePassword(long userId, ChangePasswordRequest request);
}
