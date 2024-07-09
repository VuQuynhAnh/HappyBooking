using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class SaveUserResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public SaveUserResponse(long userId, bool data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
