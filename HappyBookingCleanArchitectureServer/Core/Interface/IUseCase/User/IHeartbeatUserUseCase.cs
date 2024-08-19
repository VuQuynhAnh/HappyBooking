using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IHeartbeatUserUseCase
{
    Task<HeartbeatUserResponse> HeartbeatUser(long userId, HeartbeatUserRequest request);
}
