using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeachballData", menuName = "ScriptableObjects/BeachballData")]
public class BeachballData : ObstacleData
{
    [Tooltip("포물선에 들어갈 point의 수 (많을수록 부드러움)")]
    public int curvePoint;
    [Tooltip("움직이는 속도")]
    public float speed;
    [Tooltip("마우스로 클릭 후 해당 시간이 지나면 더이상 드래그 인식이 안됨")]
    public float draggedLimitTime;
    [Tooltip("드래그 된 이후 사라지는 시간")]
    public float disappearTimeAfterDrag;
}
