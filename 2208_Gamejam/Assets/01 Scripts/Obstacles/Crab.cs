using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crab : Obstacle
{
    private static event UnityAction crabInAction;
    private static float anotherAppearedTime = -2;

    [Space(20)]
    [SerializeField] private float speed = 1;
    [SerializeField] private float anotherCrabAppearTiming = 240;

    private Vector2 _target;

    protected override void Start()
    {
        crabInAction = null;
        anotherAppearedTime = -2;

        base.Start();
        damage = new InstantDeath();
        _target = new Vector2(SystemManager.Instance.CastlePos.x, transform.position.y);

        if(crabInAction?.GetInvocationList() == null)
        {
            if (anotherAppearedTime == -2)
            {
                anotherAppearedTime = -1;
                if (Random.Range(0, 2) == 1)
                {
                    crabInAction += () => this.SetAppear(true);
                }
            }
            else if (anotherAppearedTime == -1)
            {
                crabInAction += () => this.SetAppear(true);
            }
        }
    }

    protected override void Update()
    {
        if (anotherAppearedTime < 0)
        {
            crabInAction.Invoke();
            anotherAppearedTime = anotherAppearedTime < 0 ? appearedTime : anotherAppearedTime;
        }
        else if (!isAppear && SystemManager.Instance.timer.isTimePasses(anotherAppearedTime, anotherCrabAppearTiming))
        {
            SetAppear(true);
        }
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
        GameManager.Instance.ChangeCursorToDefense(true);
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
        GameManager.Instance.ChangeCursorToDefense(false);
    }

    public override void AfterDamage()
    {

    }
}
