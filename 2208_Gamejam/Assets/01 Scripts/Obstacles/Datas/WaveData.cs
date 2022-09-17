using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]
public class WaveData : ObstacleData
{
    [Tooltip("위험표시가 떴을 때 클릭이 가능한 리미트 시간")]
    public float limitTime = 5f;
}
