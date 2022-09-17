using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : ScriptableObject
{
    [Tooltip("랜덤 등장 시간 설정")]
    public FloatStorage appearTime;

    [Tooltip("데미지를 준 이후 딜레이되는 등장 시간")]
    public float delayAfterDamage;
}
