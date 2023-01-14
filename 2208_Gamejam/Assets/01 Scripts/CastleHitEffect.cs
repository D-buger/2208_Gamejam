using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHitEffect : MonoBehaviour
{
    private GameObject[] _effects;
    private Animator[] _effectAnim;
    private int _effectCount;

    private void Start()
    {
        _effectCount = gameObject.transform.childCount;
        _effects = new GameObject[_effectCount];
        _effectAnim = new Animator[_effectCount];
        for (int i = 0; i < _effectCount; i++)
        {
            _effects[i] = gameObject.transform.GetChild(i).gameObject;
            _effectAnim[i] = _effects[i].GetComponent<Animator>();
        }
    }

    public void HitEffect(Vector2 hitPosition)
    {
        int i = 0;
        for(; i < _effectCount; i++)
        {
            if (_effectAnim[i].GetCurrentAnimatorStateInfo(0).IsTag("disable"))
                break;
        }
        _effects[i].transform.position = hitPosition;
        _effectAnim[i].SetTrigger("HitEffect");
    }

}
