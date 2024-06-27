using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class GetListUserResponse : BaseResponse<List<UserDto>>
{
    [JsonConstructor]
    public GetListUserResponse(List<UserDto> data, StatusEnum status) : base(data, status)
    {
    }
}
