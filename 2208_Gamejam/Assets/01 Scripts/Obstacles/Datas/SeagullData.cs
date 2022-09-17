using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeagullData", menuName = "ScriptableObjects/SeagullData")]
public class SeagullData : ObstacleData
{
    public float speed = 1;
    public float moveTimeAfterAppear = 3f;
    [Range(0, 100)]
    public int percentOfAppearAnother;
}
