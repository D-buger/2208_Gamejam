using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]
public class WaveData : ObstacleData
{
    [Tooltip("����ǥ�ð� ���� �� Ŭ���� ������ ����Ʈ �ð�")]
    public float limitTime = 5f;
}
