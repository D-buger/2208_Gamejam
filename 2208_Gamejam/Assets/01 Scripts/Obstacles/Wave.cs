using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Obstacle
{
    [SerializeField] float limitTime = 5f;

    [SerializeField] GameObject surfboard;
    [SerializeField] GameObject waveObj;

    private bool _isEnableSurfboard = false;

    private Vector2 _surfboardOriPos;
    private Vector2 _waveOriPos;

    protected override void OnAwke()
    {

        if (!surfboard)
            surfboard = transform.GetChild(0).gameObject;
        if (!waveObj)
            waveObj = transform.GetChild(1).gameObject;

        waveObj.GetComponent<WaveObj>().wave = this;
        waveObj.GetComponent<WaveObj>().damage = new InstantDeath();

        _surfboardOriPos = surfboard.transform.position;
        _waveOriPos = waveObj.transform.position;
        surfboard.gameObject.SetActive(false);
        waveObj.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        damage = new NothingHappened();
        warning.remainTime = limitTime;

        warning.beforeSignEnable += () => SetAppear(true);

    }

    public override void Moving()
    {
        if(SystemManager.Instance.timer.GetGameTime > (appearedTime + limitTime))
        {
            waveObj.gameObject.SetActive(true);
        }
    }

    public override void SetAppear(bool appear)
    {
        base.SetAppear(appear);
        if (appear)
        {
            surfboard.transform.position = _surfboardOriPos;
            waveObj.transform.position = _waveOriPos;
        }
    }


    public override void MouseDown(Vector2 mousePos)
    {
        if (SystemManager.Instance.timer.GetGameTime < (appearedTime + limitTime))
        {
            surfboard.gameObject.SetActive(true);
            _isEnableSurfboard = true;
        }
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
