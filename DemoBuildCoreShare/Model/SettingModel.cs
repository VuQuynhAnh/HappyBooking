using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class SettingModel
{
    public SettingModel()
    {
    }

    public SettingModel(long userId, string languageCode)
    {
        UserId = userId;
        LanguageCode = languageCode;
    }

    public SettingModel(Setting entity)
    {
        Id = entity.Id;
        UserId = entity.UserId;
        LanguageCode = entity.LanguageCode;
    }

    public long Id { get; private set; }

    public long UserId { get; private set; }

    public string LanguageCode { get; private set; } = string.Empty;
}
