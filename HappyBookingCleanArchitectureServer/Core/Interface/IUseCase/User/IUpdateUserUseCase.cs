using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IUpdateUserUseCase
{
    Task<SaveUserResponse> UpdateUser(long userId, UpdateUserRequest request);
}
