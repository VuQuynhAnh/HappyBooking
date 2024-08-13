using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class SendMessageResponse : BaseResponse<MessageDto>
{
    [JsonConstructor]
    public SendMessageResponse(long userId, MessageDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
