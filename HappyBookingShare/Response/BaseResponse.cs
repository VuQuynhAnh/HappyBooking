using HappyBookingShare.Common;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingShare.Response;

public abstract class BaseResponse<T>
{
    [JsonIgnore]
    public IMemoryCache? Cache { get; set; }

    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    [JsonPropertyName("data")]
    public T Data { get; set; }

    [JsonPropertyName("status")]
    public StatusEnum Status { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    protected BaseResponse(long userId, T data, StatusEnum status, IMemoryCache cache)
    {
        Data = data;
        Status = status;
        Cache = cache;

        // Default language code is Vietnamese
        string languageCode = LanguageCode.VN;

        // Construct cache key based on userId and language code
        string cacheKey = $"{KeyConstant.LanguageCode}_{userId}";

        // Try to retrieve language code from cache
        if (Cache != null && Cache.TryGetValue(cacheKey, out string? cachedLanguageCode))
        {
            if (!string.IsNullOrEmpty(cachedLanguageCode))
            {
                languageCode = cachedLanguageCode;
            }
        }

        // Set message based on language code and status
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
}
