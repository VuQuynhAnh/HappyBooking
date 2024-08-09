using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class GetMessageListResponse : BaseResponse<List<MessageDto>>
{
    [JsonConstructor]
    public GetMessageListResponse(long userId, List<MessageDto> data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
