using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : ScriptableObject
{
    [Tooltip("���� ���� �ð� ����")]
    public FloatStorage appearTime;

    [Tooltip("�������� �� ���� �����̵Ǵ� ���� �ð�")]
    public float delayAfterDamage;
}
