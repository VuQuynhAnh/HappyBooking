using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingClient.Service.IService;

public interface IUserService : IBaseApiService
{
    Task<GetListUserResponse?> GetAllUserData(GetListUserRequest request);

    Task<LoginResponse?> Login(LoginRequest request);

    Task RemoveTokensFromLocalStorageAsync();

    Task<SaveUserResponse?> RegisterUser(RegisterUserRequest request);

    Task<SaveUserResponse?> UpdateUser(UpdateUserRequest request);

    Task<bool> IsTokenExpired();
}
