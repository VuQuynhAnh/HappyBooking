using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingServer.Business.IService;

public interface IUserService
{
    Task<GetListUserResponse> GetAllUserData(GetListUserRequest request);

    Task<LoginResponse> Login(LoginRequest request);

    Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
}
