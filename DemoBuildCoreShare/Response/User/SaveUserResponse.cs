using HappyBookingShare.Constant;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class SaveUserResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public SaveUserResponse(bool data, StatusEnum status) : base(data, status)
    {
    }
}
