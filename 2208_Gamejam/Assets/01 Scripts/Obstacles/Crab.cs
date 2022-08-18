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
    [SerializeField] private float anotherCrabAppearTiming = 20;

    [SerializeField] private AudioClip crabClickSound;

    private Animator _anim;
    private Vector2 _target;

    private bool _isGoBack = false;

    protected override void OnAwke()
    {
        crabInAction = null;
        if (anotherAppearedTime == 0)
            anotherAppearedTime = -2;
        _anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        _isGoBack = false;
        damage = new InstantDeath(() =>
        {
            deathCamera.SetActive(true);
        });
        _target = new Vector2(SystemManager.Instance.CastlePos.x, transform.position.y);
        _anim.SetBool("isGrabbed", false);

        if (crabInAction?.GetInvocationList() == null)
        {
            if (anotherAppearedTime == -2)
            {
                anotherAppearedTime = -1;
                if (Random.Range(0, 2) == 1)
                {
                    crabInAction += () => this.SetAppear(true);
                    anotherAppearedTime = 0;
                }
            }
            else if (anotherAppearedTime == -1)
            {
                crabInAction += () => this.SetAppear(true);
                anotherAppearedTime = 0;
            }
        }
    }

    protected override void Update()
    {
        if (anotherAppearedTime <= 0)
        {
            crabInAction?.Invoke();
            crabInAction = null;
            anotherAppearedTime = anotherAppearedTime <= 0 ? appearedTime : anotherAppearedTime;
        }

        if (!isAppear && SystemManager.Instance.timer.isTimePasses(anotherAppearedTime, anotherCrabAppearTiming))
        {
            SetAppear(true);
        }
    }

    public override void Moving()
    {
        if (!_isGoBack)
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        else
        {
            if (Vector2.Distance(transform.position, oriPos) < 0.5f)
                _isGoBack = false;
            transform.position = Vector3.MoveTowards(transform.position, oriPos, speed * 10f * Time.deltaTime);
        }

    }


    private Vector2 mouseDownPos = Vector2.zero;
    public override void MouseDown(Vector2 mousePos)
    {
        mouseDownPos = mousePos;
        isPlay = false;
        GameManager.Instance.ChangeCursorToDefense(true);
        SetAudioAndPlay(crabClickSound);
    }

    public override void OnDrag(Vector2 mousePos)
    {
        float min = mouseDownPos.x > oriPos.x ? oriPos.x : mouseDownPos.x;
        float max = mouseDownPos.x < oriPos.x ? oriPos.x : mouseDownPos.x;
        transform.position = new Vector2(Mathf.Clamp(mousePos.x, min, max), transform.position.y);
        _anim.SetBool("isGrabbed", true);
    }

    public override void MouseUp(Vector2 mousePos)
    {
        isPlay = true;
        GameManager.Instance.ChangeCursorToDefense(false);
        _anim.SetBool("isGrabbed", false);
    }

    public override void AfterDamage()
    {
        _isGoBack = true;
    }
}
