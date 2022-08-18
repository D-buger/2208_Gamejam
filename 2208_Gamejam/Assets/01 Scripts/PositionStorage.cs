using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionStorage
{
    public PositionStorage(Vector2 LD, Vector2 RU)
    {
        _leftDownVec = LD;
        _rightUpVec = RU;
    }

    [SerializeField] Transform leftDown;
    [SerializeField] Transform rightUp;
    Vector2 _leftDownVec;
    Vector2 _rightUpVec;

    public Vector2 GetRandomPosition()
    {
        if (leftDown)
        {
            _leftDownVec = leftDown.position;
            _rightUpVec = rightUp.position;
        }

        return _rightUpVec.Equals(Vector2.zero) ? _leftDownVec : new Vector2(RandomUtilities.Random(_leftDownVec.x, _rightUpVec.x), RandomUtilities.Random(_leftDownVec.y, _rightUpVec.y));
    }
}
