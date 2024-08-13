using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Chat;

public class GetListChatGroupByMemberResponse : BaseResponse<List<ChatDto>>
{
    [JsonConstructor]
    public GetListChatGroupByMemberResponse(long userId, List<ChatDto> data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
