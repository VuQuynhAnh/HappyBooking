using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.ImageUpload;

public class DeleteImageResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public DeleteImageResponse(long userId, bool data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
