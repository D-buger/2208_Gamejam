using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _stopwatch;

    public Timer() => _stopwatch = 0;
    public void TimeUpdate()
    {
        _stopwatch += Time.deltaTime;
    }

    public float GetGameTime => _stopwatch;
    public bool isTimePasses(float settedTime) => _stopwatch > settedTime;
}
