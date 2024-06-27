using HappyBookingShare.Common;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response;

public abstract class BaseResponse<T>
{
    protected BaseResponse(T data, StatusEnum status)
    {
        Data = data;
        Status = status;
        Message = MessageConstant.GetMessage(status);
    }

    [JsonPropertyName("data")]
    public T Data { get; set; }

    [JsonPropertyName("status")]
    public StatusEnum Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
