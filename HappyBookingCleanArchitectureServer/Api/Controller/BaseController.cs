using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Authorize]
public abstract class BaseController : ControllerBase
{
    protected long UserId { get; }
    private readonly IHeartbeatUserUseCase _heartbeatUserUseCase;
    private readonly IHubContext<ChatHub> _hubContext;
    private static readonly Dictionary<long, DateTime> lastSignalRUpdateMap = new();
    private static readonly object updateLock = new object();


    protected BaseController(IHttpContextAccessor httpContextAccessor, IHeartbeatUserUseCase heartbeatUserUseCase, IHubContext<ChatHub> hubContext)
    {
        var userIdString = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        long.TryParse(userIdString, out long userId);
        UserId = userId;
        _heartbeatUserUseCase = heartbeatUserUseCase;
        _hubContext = hubContext;
    }

    protected async Task HeartbeatUser()
    {
        var now = DateTime.UtcNow;
        lock (updateLock)
        {
            if (lastSignalRUpdateMap.TryGetValue(UserId, out var lastUpdate) &&
                (now - lastUpdate).TotalSeconds < ParamConstant.LastSecond)
            {
                return;
            }
            lastSignalRUpdateMap[UserId] = now;
        }
        await _heartbeatUserUseCase.HeartbeatUser(UserId, _hubContext);
    }
}
