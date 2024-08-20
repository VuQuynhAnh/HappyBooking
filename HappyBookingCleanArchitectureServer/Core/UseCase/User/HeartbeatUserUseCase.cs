using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.User;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

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

    public async Task<HeartbeatUserResponse> HeartbeatUser(long userId, IHubContext<ChatHub> hubContext)
    {
        await UpdateStatus(hubContext);
        UserDto result = new();
        var heartbeatUserResult = await _userRepository.HeartbeatUser(userId);
        if (heartbeatUserResult != null)
        {
            result = new UserDto(heartbeatUserResult);
            string jsonString = JsonSerializer.Serialize(result);
            await hubContext.Clients.All.SendAsync(RealtimeConstant.UserOnline, jsonString);
        }
        return new HeartbeatUserResponse(userId, result, StatusEnum.Successed, _cache);
    }

    private async Task UpdateStatus(IHubContext<ChatHub> hubContext)
    {
        List<long> result = await _userRepository.AutoMarkUserAsOffline(ParamConstant.LastSecond);
        string jsonString = JsonSerializer.Serialize(result);
        await hubContext.Clients.All.SendAsync(RealtimeConstant.UserOffline, jsonString);
    }
}
