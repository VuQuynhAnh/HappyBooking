using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.ImageUpload;

public class UploadImageResponse : BaseResponse<string>
{
    [JsonConstructor]
    public UploadImageResponse(long userId, string data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
