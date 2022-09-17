using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeachballData", menuName = "ScriptableObjects/BeachballData")]
public class BeachballData : ObstacleData
{
    public int curvePoint;
    public float speed;
    public float draggedLimitTime;
    public float disappearTimeAfterDrag;
}
