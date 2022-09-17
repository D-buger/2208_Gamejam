using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrabData", menuName = "ScriptableObjects/CrabData")]
public class CrabData : ObstacleData
{
    [Tooltip("움직이는 시간")]
    public float speed = 1;
    [Tooltip("나머지 게가 등장할 시간 (게임 시작 이후)")]
    public float crabAppearTiming = 90;
}
