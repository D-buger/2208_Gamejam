using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator _effectAnim;

    private void Awake()
    {
        _effectAnim = GetComponent<Animator>();
    }


    private void FollowMouse()
    {
        Vector2 mousePos = new Vector2(SystemManager.Instance.input.MousePos.x, SystemManager.Instance.input.MousePos.y);
        mousePos.x -= GameManager.Instance.cursorHotspot.x;
        mousePos.y -= GameManager.Instance.cursorHotspot.y;
        transform.position = mousePos;
    }

    public void BeachballEffect()
    {
        FollowMouse();
        _effectAnim.SetTrigger("beachballTrigger");
    }

}
