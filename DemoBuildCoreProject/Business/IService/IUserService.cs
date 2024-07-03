using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace DemoBuildCoreProject.Business.IService;

public interface IUserService
{
    Task<GetListUserResponse> GetAllUserData(GetListUserRequest request);

    Task<LoginResponse> Login(LoginRequest request);

    Task<LoginResponse> RefreshToken(RefreshTokenRequest request);

    Task<SaveUserResponse> RegisterUser(long userId, RegisterUserRequest request);

    Task<SaveUserResponse> UpdateUser(long userId, UpdateUserRequest request);
}
