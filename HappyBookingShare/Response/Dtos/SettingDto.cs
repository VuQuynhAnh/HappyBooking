using HappyBookingShare.Model;

namespace HappyBookingShare.Response.Dtos;

public class SettingDto
{
    public SettingDto(SettingModel model)
    {
        Id = model.Id;
        UserId = model.UserId;
        LanguageCode = model.LanguageCode;
    }

    public long Id { get; private set; }

    public long UserId { get; private set; }

    public string LanguageCode { get; private set; } = string.Empty;
}
