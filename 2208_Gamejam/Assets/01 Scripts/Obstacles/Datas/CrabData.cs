using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrabData", menuName = "ScriptableObjects/CrabData")]
public class CrabData : ObstacleData
{
    public float speed = 1;
    public float crabAppearTiming = 90;
}
