using System;
using System.Collections.Generic;

[Serializable]
public class LocalizationDictionary
{
    public List<LocalizationItem> localizationStrings;

    public LocalizationDictionary()
    {
        localizationStrings = new List<LocalizationItem>();
    }

    public LocalizationDictionary(Dictionary<string, string> dictionary)
    {
        localizationStrings = new List<LocalizationItem>();
        foreach (var kvp in dictionary)
        {
            localizationStrings.Add(new LocalizationItem(kvp.Key, kvp.Value));
        }
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (var kvp in localizationStrings)
        {
            dictionary[kvp.key] = kvp.value;
        }
        return dictionary;
    }
}