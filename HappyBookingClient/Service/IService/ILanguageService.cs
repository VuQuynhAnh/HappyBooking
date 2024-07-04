using System.Globalization;

namespace HappyBookingClient.Service.IService;

public interface ILanguageService
{
    CultureInfo CurrentCulture { get; }

    Task SetLanguage(string languageCode);

    Task LoadLanguage();

    string this[string key] { get; }

    event Action OnChange;
}
