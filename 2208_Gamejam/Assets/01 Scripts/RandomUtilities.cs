using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtilities
{
    public static System.Random RNGSingleton = new System.Random();

    public static int Random(int min, int max)
    {
        return RandomUtilities.RNGSingleton.Next(min, max);
    }
    public static float Random(float min, float max)
    {
        float random = Random((int)min, (int)max);
        float randomDcimal = (float)RandomUtilities.RNGSingleton.NextDouble();
        if(randomDcimal > max % 1 && random == (int)max)
        {
            randomDcimal = max % 1;
        }
        else if(randomDcimal < min % 1 && random == (int)min)
        {
            randomDcimal = min % 1;
        }
        return random + randomDcimal;
    }
}
