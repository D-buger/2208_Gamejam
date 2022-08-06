using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seagull : Obstacle
{
    static event UnityAction seagullOutAction;

    [Space(20)]
    [SerializeField] private float speed = 1;
    [SerializeField] private float moveTimeAfterAppear = 3f;

    private Vector2 _target;

    private void Start()
    {
        seagullOutAction += () => this.SetAppear(false);
        _damage = new JustDamage();
        _target = SystemManager.Instance.CastlePos;
    }

    public override void Moving()
    {
        if(SystemManager.Instance.timer.GetGameTime - appearedTime > moveTimeAfterAppear)
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }


    public override void MouseDown(Vector2 mousePos)
    {
        SetAppear(false);
    }
    public override void OnDrag(Vector2 mousePos)
    {

    }
    public override void MouseUp(Vector2 mousePos)
    {

    }

    public override void AfterDamage()
    {
        seagullOutAction?.Invoke();
    }
}
