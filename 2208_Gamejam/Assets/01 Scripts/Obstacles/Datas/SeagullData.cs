using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeagullData", menuName = "ScriptableObjects/SeagullData")]
public class SeagullData : ObstacleData
{
    [Tooltip("움직이는 속도")]
    public float speed = 1;
    [Tooltip("등장한 이후 움직이기 시작하는 시간")]
    public float moveTimeAfterAppear = 3f;

    [Tooltip("다른 갈매기들이 나올 확률 (1마리는 기본, 높을수록 갈매기가 많이 나올 확률이 높아짐)")]
    [Range(0, 100)]
    public int percentOfAppearAnother;
}
