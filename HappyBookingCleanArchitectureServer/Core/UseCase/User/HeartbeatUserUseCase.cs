using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.User;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.User;

public class HeartbeatUserUseCase : IHeartbeatUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public HeartbeatUserUseCase(IUserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<HeartbeatUserResponse> HeartbeatUser(long userId, HeartbeatUserRequest request)
    {
        try
        {
            var result = await _userRepository.HeartbeatUser(request.UserId);
            return new HeartbeatUserResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }
    }
}
