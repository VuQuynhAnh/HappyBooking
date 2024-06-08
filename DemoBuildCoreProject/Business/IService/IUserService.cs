using DemoBuildCoreProject.Request.User;
using DemoBuildCoreProject.Response.User;

namespace DemoBuildCoreProject.Business.IService;

public interface IUserService
{
    Task<GetListUserResponse> GetAllUserData(GetListUserRequest request);

    Task<LoginResponse> Login(LoginRequest request);

    Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
}
