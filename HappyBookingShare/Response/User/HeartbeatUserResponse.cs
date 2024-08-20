using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class HeartbeatUserResponse : BaseResponse<List<UserDto>>
{
    [JsonConstructor]
    public HeartbeatUserResponse(long userId, List<UserDto> data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}