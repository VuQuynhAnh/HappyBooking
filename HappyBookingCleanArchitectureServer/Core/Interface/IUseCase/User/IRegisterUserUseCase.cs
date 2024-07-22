using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IRegisterUserUseCase
{
    Task<SaveUserResponse> RegisterUser(long userId, RegisterUserRequest request);
}
