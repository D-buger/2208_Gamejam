using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatStorage
{
    [SerializeField] private float randomStart;
    [SerializeField] private float randomEnd = 0;

    public float GetRandomFloat() 
        => randomEnd != 0 ? Random.Range(randomStart, randomEnd) : randomStart;
}
