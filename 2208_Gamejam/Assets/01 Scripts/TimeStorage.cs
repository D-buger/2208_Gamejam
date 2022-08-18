using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatStorage
{
    [SerializeField] private float randomStart;
    [SerializeField] private float randomEnd = 0;

    public float GetRandomFloat()
    {
        float f = RandomUtilities.Random(randomStart, randomEnd);
        return randomEnd != 0 ? f : randomStart;
    }

    public int GetRandomInt()
    {
        int random = randomEnd != 0 ? RandomUtilities.Random((int)randomStart, (int)randomEnd) : (int)randomStart;
        return random;
    }
}
