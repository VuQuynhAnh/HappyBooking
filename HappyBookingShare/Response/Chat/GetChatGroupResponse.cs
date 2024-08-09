using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class GetChatGroupResponse : BaseResponse<ChatDto>
{
    [JsonConstructor]
    public GetChatGroupResponse(long userId, ChatDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
