using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrabData", menuName = "ScriptableObjects/CrabData")]
public class CrabData : ObstacleData
{
    [Tooltip("�����̴� �ð�")]
    public float speed = 1;
    [Tooltip("������ �԰� ������ �ð� (���� ���� ����)")]
    public float crabAppearTiming = 90;
}
