using System.Globalization;
using System.Text.Json;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Common;

namespace HappyBookingClient.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingService _settingService;
        private readonly IConfiguration _configuration;

        public LanguageService(HttpClient httpClient, ISettingService settingService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _settingService = settingService;
            _configuration = configuration;
        }

        public CultureInfo CurrentCulture { get; private set; } = new CultureInfo(LanguageCode.VN);
        public Dictionary<string, string> Translations { get; private set; } = new();

        public event Action OnChange;

        public async Task LoadLanguage()
        {
            string languageCode = LanguageCode.VN;
            var setting = await _settingService.GetSetting();
            if (!string.IsNullOrEmpty(setting?.Data.LanguageCode))
            {
                languageCode = setting.Data.LanguageCode;
            }
            CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.DefaultThreadCurrentCulture = CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CurrentCulture;

            try
            {
                var baseUrl = _configuration["ApiSettings:BaseAddress"];
                var json = await _httpClient.GetStringAsync($"{baseUrl}Resources/{languageCode}.json");
                Translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error loading language file: {ex.Message}");
            }

            OnChange?.Invoke();
        }

        public string this[string key] => Translations.TryGetValue(key, out var value) ? value : key;
    }
}
