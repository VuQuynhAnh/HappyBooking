using HappyBookingShare.Common;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.ImageUpload;

public class UploadImageResponse : BaseResponse<string>
{
    [JsonConstructor]
    public UploadImageResponse(string data, StatusEnum status) : base(data, status)
    {
    }
}
