using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;

    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("TimerManager").AddComponent<TimerManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Coroutine StartTimer(float duration, System.Action onTimerComplete)
    {
        return StartCoroutine(TimerCoroutine(duration, onTimerComplete));
    }

    private IEnumerator TimerCoroutine(float duration, System.Action onTimerComplete)
    {
        yield return new WaitForSeconds(duration);
        onTimerComplete?.Invoke();
    }
}
