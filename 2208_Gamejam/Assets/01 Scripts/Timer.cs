using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    public static float realDeltaTime { get; private set; } = 0;
    public static float realTime { get; private set; } = 0;

    public event UnityAction<float> timeEvent;
    private float _stopwatch;

    public Timer()
    {
        _stopwatch = 0;
        realTime = Time.realtimeSinceStartup;
    }
    public Timer(UnityAction<float> action) : base() => timeEvent += action;
    public void TimeUpdate()
    {
        realDeltaTime = Time.realtimeSinceStartup - realTime;
        realTime = Time.realtimeSinceStartup;

        _stopwatch += Time.deltaTime;
        timeEvent.Invoke(_stopwatch);
    }

    public float GetGameTime => _stopwatch;
    public bool isTimePasses(float settedTime) => _stopwatch > settedTime;
    public bool isTimePasses(float oldTime,float settedTime) => _stopwatch > oldTime + settedTime;
}
