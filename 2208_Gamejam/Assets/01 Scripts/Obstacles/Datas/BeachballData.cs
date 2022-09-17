using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeachballData", menuName = "ScriptableObjects/BeachballData")]
public class BeachballData : ObstacleData
{
    [Tooltip("�������� �� point�� �� (�������� �ε巯��)")]
    public int curvePoint;
    [Tooltip("�����̴� �ӵ�")]
    public float speed;
    [Tooltip("���콺�� Ŭ�� �� �ش� �ð��� ������ ���̻� �巡�� �ν��� �ȵ�")]
    public float draggedLimitTime;
    [Tooltip("�巡�� �� ���� ������� �ð�")]
    public float disappearTimeAfterDrag;
}
