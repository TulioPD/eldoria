using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    public Image fadeImage;

    private void Awake()
    {
        FadeFromBlack(2f);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeToBlack(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0f);
        fadeImage.CrossFadeAlpha(1f, duration, false);
    }

    public void FadeFromBlack(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(1f);
        fadeImage.CrossFadeAlpha(0f, duration, false);
    }
}
