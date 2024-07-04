using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Setting;

public class GetSettingResponse : BaseResponse<SettingDto>
{
    [JsonConstructor]
    public GetSettingResponse(SettingDto data, StatusEnum status) : base(data, status)
    {
    }
}
