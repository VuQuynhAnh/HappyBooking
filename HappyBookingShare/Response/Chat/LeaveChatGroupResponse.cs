using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class LeaveChatGroupResponse : BaseResponse<ChatDto>
{
    [JsonConstructor]
    public LeaveChatGroupResponse(long userId, ChatDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
