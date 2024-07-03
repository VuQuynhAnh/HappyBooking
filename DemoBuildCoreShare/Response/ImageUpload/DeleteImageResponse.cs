using HappyBookingShare.Common;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.ImageUpload;

public class DeleteImageResponse : BaseResponse<bool>
{
    [JsonConstructor]
    public DeleteImageResponse(bool data, StatusEnum status) : base(data, status)
    {
    }
}
