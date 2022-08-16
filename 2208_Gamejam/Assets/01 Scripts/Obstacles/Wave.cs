using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Obstacle
{
    [SerializeField] float limitTime = 5f;

    protected override void Start()
    {
        base.Start();
        damage = new InstantDeath();
        warning.remainTime = limitTime;

        warning.beforeSignEnable += () => SetAppear(true);
        
    }
    public override void Moving()
    {
        if(SystemManager.Instance.timer.GetGameTime > (appearedTime + limitTime))
        {
            damage.DamageToPlayer();
        }
    }

    public override void SetAppear(bool appear)
    {
        base.SetAppear(appear);
        if (appear)
        {
            _renderer.enabled = false;
        }
    }


    public override void MouseDown(Vector2 mousePos)
    {
        SetAppear(false);
        warning.WarningSignDisable();
    }
    public override void OnDrag(Vector2 mousePos)
    {

    }
    public override void MouseUp(Vector2 mousePos)
    {

    }


    public override void AfterDamage()
    {

    }
}
