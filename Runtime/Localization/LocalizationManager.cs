using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class LocalizationManager
{
    private static Dictionary<string, string> _localizationData;

    public static SystemLanguage CurrentLanguage { get; private set; }

    public static string Language => GetLanguageName(CurrentLanguage);

    public static async Task SetLanguageAsync(SystemLanguage systemLanguage)
    {
        var languageName = GetLanguageShortName(systemLanguage);
        var localizationAsset = await Addressables.LoadAssetAsync<TextAsset>($"lang.{languageName}").Task;
        _localizationData = JsonUtility.FromJson<LocalizationDictionary>(localizationAsset.text)
                                       .ToDictionary();
        CurrentLanguage = systemLanguage;
    }

    public static string GetLocalizedString(string key)
    {
        if (_localizationData is not null && _localizationData.TryGetValue(key, out var value))
        {
            return value;
        }
        return key;
    }

    private static string GetLanguageShortName(SystemLanguage systemLanguage)
    {
        return _languages.FirstOrDefault(l => l.lang == systemLanguage).shortname ?? "en";
    }

    private static string GetLanguageName(SystemLanguage systemLanguage)
    {
        return _languages.FirstOrDefault(l => l.lang == systemLanguage).name ?? "ENGLISH";
    }

    private static (SystemLanguage lang, string name, string shortname)[] _languages = new[]
    {
            (SystemLanguage.English, "ENGLISH", "en"),
            (SystemLanguage.Russian, "РУССКИЙ", "ru"),
        };

    public static List<string> GetAvaliableLanguages()
    {
        return _languages.Select(l => l.name).ToList();
    }

    public static SystemLanguage GetLanguage(string languageName)
    {
        var lang = _languages.FirstOrDefault(l => l.name == languageName);

        return lang == default ? SystemLanguage.English : lang.lang;
    }

}