using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Obstacle
{
    [Space(20)]
    [SerializeField] private float speed = 1;

    private Vector2 _target;

    private void Start()
    {
        //_damage = new InstantDeath();
        _damage = new JustDamage();
        _target = new Vector2(SystemManager.Instance.CastlePos.x, transform.position.y);
    }

    public override void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }


    private Vector2 mouseDownPos = Vector2.zero;
    public override void MouseDown(Vector2 mousePos)
    {
        mouseDownPos = mousePos;
        isPlay = false;
    }

    public override void OnDrag(Vector2 mousePos)
    {
        float min = mouseDownPos.x > oriPos.x ? oriPos.x : mouseDownPos.x;
        float max = mouseDownPos.x < oriPos.x ? oriPos.x : mouseDownPos.x;
        transform.position = new Vector2(Mathf.Clamp(mousePos.x, min, max), transform.position.y);
    }

    public override void MouseUp(Vector2 mousePos)
    {
        isPlay = true;
    }

    public override void AfterDamage()
    {

    }
}
