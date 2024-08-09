using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class LeaveChatGroupResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public LeaveChatGroupResponse(long userId, bool data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
