using HappyBookingShare.Model;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Dtos;

public class SettingDto
{
    public SettingDto(SettingModel model)
    {
        Id = model.Id;
        UserId = model.UserId;
        LanguageCode = model.LanguageCode;
    }

    public SettingDto()
    {
    }

    [JsonConstructor]
    public SettingDto(long id, long userId, string languageCode)
    {
        Id = id;
        UserId = userId;
        LanguageCode = languageCode;
    }

    [JsonPropertyName("id")]
    public long Id { get; private set; }

    [JsonPropertyName("userId")]
    public long UserId { get; private set; }

    [JsonPropertyName("languageCode")]
    public string LanguageCode { get; private set; } = string.Empty;
}
