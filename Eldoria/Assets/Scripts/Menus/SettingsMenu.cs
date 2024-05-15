using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown, qualityDropdown, languageDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        InitializeResolutionsDropdown();
        InitializeQualityDropdown();
        InitializeLanguageDropdown();
    }

    private void InitializeResolutionsDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void InitializeQualityDropdown()
    {
        string[] qualityLevels = QualitySettings.names;
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(qualityLevels));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    private void InitializeLanguageDropdown()
    {
        if (LocalizationSettings.AvailableLocales.Locales.Count > 0)
        {
            List<string> languages = new List<string>();
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                languages.Add(locale.name);
            }
            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(languages);

            int selectedIndex = GetSelectedLocaleIndex();
            languageDropdown.value = selectedIndex;
            languageDropdown.RefreshShownValue();
        }
    }

    private int GetSelectedLocaleIndex()
    {
        var selectedLocaleId = LocalizationSettings.SelectedLocale.Identifier.Code;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            if (LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code == selectedLocaleId)
            {
                return i;
            }
        }
        return 0;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ChangeLanguage(int index)
    {
        var selectedLocaleId = LocalizationSettings.AvailableLocales.Locales[index].Identifier.Code;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(selectedLocaleId);
    }

    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
