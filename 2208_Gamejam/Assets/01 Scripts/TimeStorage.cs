using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeStorage
{
    [SerializeField] private float randomStartTime;
    [SerializeField] private float randomEndTime = 0;

    public float GetRandomTime() 
        => randomEndTime != 0 ? Random.Range(randomStartTime, randomEndTime) : randomStartTime;
}
