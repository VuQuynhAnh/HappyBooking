using HappyBookingClient.Service.IService;
using System.Globalization;
using System.Text.Json;
using System;
using HappyBookingShare.Common;

namespace HappyBookingClient.Service;

public class LanguageService : ILanguageService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LanguageService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public CultureInfo CurrentCulture { get; private set; } = new CultureInfo(LanguageCode.VN);

    public Dictionary<string, string> Translations { get; private set; } = new Dictionary<string, string>();

    public event Action OnChange;

    public async Task SetLanguage(string languageCode)
    {
        CurrentCulture = new CultureInfo(languageCode);
        CultureInfo.DefaultThreadCurrentCulture = CurrentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CurrentCulture;

        var client = _httpClientFactory.CreateClient();
        var json = await client.GetStringAsync($"Resources/{languageCode}.json");
        Translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

       

        OnChange?.Invoke();
    }

    public async Task LoadLanguage()
    {
        //await SetLanguage(languageCode);
    }

    public string this[string key] => Translations.TryGetValue(key, out var value) ? value : key;
}
