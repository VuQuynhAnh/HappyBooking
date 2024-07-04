using HappyBookingShare.Common;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Setting;

public class SaveSettingResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public SaveSettingResponse(bool data, StatusEnum status) : base(data, status)
    {
    }
}
