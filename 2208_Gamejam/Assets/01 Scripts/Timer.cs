using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _stopwatch;
    public event UnityAction<float> timeEvent;

    public Timer() => _stopwatch = 0;
    public Timer(UnityAction<float> action) : base() => timeEvent += action;
    public void TimeUpdate()
    {
        _stopwatch += Time.deltaTime;
        timeEvent.Invoke(_stopwatch);
    }

    public float GetGameTime => _stopwatch;
    public bool isTimePasses(float settedTime) => _stopwatch > settedTime;
}
