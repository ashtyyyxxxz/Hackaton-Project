using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerTime;
    [SerializeField] private float remainTime;

    public UnityEvent onTimerEnd;

    public IEnumerator StartTimer()
    {
        remainTime = timerTime;
        while (remainTime > 0)
        {
            remainTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        onTimerEnd?.Invoke();
    }
}
