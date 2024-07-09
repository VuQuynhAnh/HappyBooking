using HappyBookingShare.Common;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingShare.Response;

public abstract class BaseResponse<T>
{
    protected BaseResponse(long userId, T data, StatusEnum status, IMemoryCache cache)
    {
        Data = data;
        Status = status;
        string? languageCode;
        string cacheKey = $"{KeyConstant.LanguageCode}_{userId}";
        if (!cache.TryGetValue(cacheKey, out languageCode))
        {
            languageCode = LanguageCode.VN;
        }
        switch (languageCode)
        {
            case LanguageCode.VN:
                Message = VietnameseMessageConstant.GetMessage(status);
                break;
            default:
                Message = EnglishMessageConstant.GetMessage(status);
                break;
        }
    }

    [JsonPropertyName("data")]
    public T Data { get; set; }

    [JsonPropertyName("status")]
    public StatusEnum Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
