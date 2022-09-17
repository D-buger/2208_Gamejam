using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeagullData", menuName = "ScriptableObjects/SeagullData")]
public class SeagullData : ObstacleData
{
    [Tooltip("�����̴� �ӵ�")]
    public float speed = 1;
    [Tooltip("������ ���� �����̱� �����ϴ� �ð�")]
    public float moveTimeAfterAppear = 3f;

    [Tooltip("�ٸ� ���ű���� ���� Ȯ�� (1������ �⺻, �������� ���űⰡ ���� ���� Ȯ���� ������)")]
    [Range(0, 100)]
    public int percentOfAppearAnother;
}
