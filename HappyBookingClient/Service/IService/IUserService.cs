using HappyBookingShare.Request.Auth;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Auth;
using HappyBookingShare.Response.User;

namespace HappyBookingClient.Service.IService;

public interface IUserService : IBaseApiService
{
    Task<GetListUserResponse?> GetAllUserData(GetListUserRequest request);

    Task<GetUserByUserIdResponse?> GetUserByUserId(long userId);

    Task<LoginResponse?> Login(LoginRequest request);

    Task ClearAllLocalStorage();

    Task<SaveUserResponse?> RegisterUser(RegisterUserRequest request);

    Task<SaveUserResponse?> UpdateUser(UpdateUserRequest request);

    Task<SaveUserResponse?> ChangePassword(ChangePasswordRequest request);

    Task<bool> IsTokenExpired();
}
