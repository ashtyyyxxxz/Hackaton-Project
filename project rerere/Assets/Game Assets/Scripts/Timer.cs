using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerTime;
    [SerializeField] private float remainTime;

    [SerializeField] private TextMeshPro timerText;

    public UnityEvent onTimerEnd;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        StartCoroutine(StartTimerRoutine());
    }

    private IEnumerator StartTimerRoutine()
    {
        remainTime = timerTime;
        while (remainTime > 0)
        {
            remainTime -= 1f;
            yield return new WaitForSeconds(1f);

            if(timerText != null)
            {
                timerText.text = $"Осталось {remainTime} \n секунд ";
            }
        }
        onTimerEnd?.Invoke();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
