using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;

public interface ILoginUseCase
{
    Task<LoginResponse> Login(LoginRequest request);
}
