using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.Status;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.User;

public class GetListUserResponse : IResponse<List<UserDto>>
{
    [JsonConstructor]
    public GetListUserResponse(List<UserDto> data, string message, CommonStatus status)
    {
        Data = data;
        Message = message;
        Status = status;
    }

    [JsonPropertyName("data")]
    public List<UserDto> Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("status")]
    public CommonStatus Status { get; set; }
}
