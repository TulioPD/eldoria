using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public enum FullscreenMode
    {
        FullScreen,
        Windowed,
    }
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    [HideInInspector] public int currentScreenWidth;
    [HideInInspector] public int currentScreenHeight;


    [HideInInspector] public FullscreenMode currentFullscreenMode;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i =0; i< resolutions.Length; i++)
        {
            string option= resolutions[i].width+ "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height==Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        SetFullscreenMode(FullscreenMode.FullScreen);
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

    public void SetFullscreenMode(FullscreenMode mode)
    {
        switch (mode)
        {
            case FullscreenMode.FullScreen:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case FullscreenMode.Windowed:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }

        currentFullscreenMode = mode;
    }

    public void ToggleFullscreen()
    {
        switch (currentFullscreenMode)
        {
            case FullscreenMode.FullScreen:
                SetFullscreenMode(FullscreenMode.Windowed);
                break;
            case FullscreenMode.Windowed:
                SetFullscreenMode(FullscreenMode.FullScreen);
                break;
        }
    }

    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
