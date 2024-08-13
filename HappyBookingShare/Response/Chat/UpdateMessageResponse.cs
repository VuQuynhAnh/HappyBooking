using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class UpdateMessageResponse : BaseResponse<MessageDto>
{
    [JsonConstructor]
    public UpdateMessageResponse(long userId, MessageDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
