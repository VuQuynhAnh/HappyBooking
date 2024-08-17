using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class SaveChatGroupResponse : BaseResponse<ChatDto>
{
    [JsonConstructor]
    public SaveChatGroupResponse(long userId, ChatDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}