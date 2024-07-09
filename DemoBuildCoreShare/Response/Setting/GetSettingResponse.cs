using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Setting;

public class GetSettingResponse : BaseResponse<SettingDto>
{
    [JsonConstructor]
    public GetSettingResponse(long userId, SettingDto data, StatusEnum status, IMemoryCache cache) : base(userId, data, status, cache)
    {
    }
}
