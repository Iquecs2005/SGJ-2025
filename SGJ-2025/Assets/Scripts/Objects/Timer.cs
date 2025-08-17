using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerDuration;
    [SerializeField] private bool startOnEnabled;

    [SerializeField] private UnityEvent OnTimerCompleted;

    private bool running = false;
    private float timeElapsed;

    private void OnEnable()
    {
        if (startOnEnabled)
            StartTimer();
    }

    public void StartTimer() 
    {
        if (running) return;

        timeElapsed = 0;
        running = true;
    }

    public void StopTimer() 
    {
        timeElapsed = 0;
        running = false;
    }

    private void FixedUpdate()
    {
        if (!running) return;

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timerDuration) 
        {
            StopTimer();
            OnTimerCompleted.Invoke();
        }
    }
}
