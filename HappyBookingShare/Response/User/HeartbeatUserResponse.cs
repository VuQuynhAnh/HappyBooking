using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class HeartbeatUserResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public HeartbeatUserResponse(long userId, bool data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}