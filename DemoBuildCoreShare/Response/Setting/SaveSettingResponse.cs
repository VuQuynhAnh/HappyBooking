using HappyBookingShare.Common;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Setting;

public class SaveSettingResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public SaveSettingResponse(long userId, bool data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
