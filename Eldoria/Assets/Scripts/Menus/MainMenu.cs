using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
    }

    public void ControlsMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Guide");
    }
}
