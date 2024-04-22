using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public float fadeDuration = 2f;
    public string nextSceneName;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            InputHandler inputHandler = other.GetComponent<InputHandler>();
            if (inputHandler != null)
            {
                inputHandler.enabled = false;
            }

            TimerManager.Instance.StartTimer(1f, TransitionToNextScene);
        }
    }

    private void TransitionToNextScene()
    {
        ScreenFader.Instance.FadeToBlack(fadeDuration);
        TimerManager.Instance.StartTimer(fadeDuration, LoadNextScene);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
