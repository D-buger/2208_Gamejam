using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//TODO : fadeout 구현 및 제자리 이동하는 모션 추가
public class Seagull : Obstacle
{
    private static event UnityAction _seagullOutAction;
    private static event UnityAction _seagullInAction;

    private static event UnityAction _seagullAllAction;

    private static int appearedSeagullNum = 0;

    [SerializeField] private Sprite[] seagullSprite;
    [Space(20)]
    [SerializeField] private float speed = 1;
    [SerializeField] private float moveTimeAfterAppear = 3f;
    [Range(0, 100)]
    [SerializeField] private int percentToAppearAnother;

    private Vector2 _target;

    private static float _appearedTime;
    private static float _disappearedTime;

    protected override void Start()
    {
        _seagullOutAction = null;
        _seagullAllAction = null;
        _seagullInAction = null;
        base.Start();
        _seagullOutAction += () => this.SetAppear(false);
        _seagullOutAction += SetDelegate;


        damage = new JustDamage();
        _target = SystemManager.Instance.CastlePos;

        _seagullAllAction += () => this.SetAppear(true);

        SetDelegate();
    }

    private void GoBackToOriPosAndDisable()
    {
        this.SetAppear(false);
    }

    protected override void Update()
    {
        if (_seagullInAction != null && !isAppear && SystemManager.Instance.timer.isTimePasses(_disappearedTime + delayAfterDamage, appearTime.GetRandomFloat()))
        {
            _seagullInAction?.Invoke();
            _seagullInAction = null;

            _appearedTime = SystemManager.Instance.timer.GetGameTime;
        }
    }

    public override void Moving()
    {
        if(SystemManager.Instance.timer.GetGameTime - _appearedTime > moveTimeAfterAppear)
            transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public void SetDisable()
    {
        SetAppear(false);

        if (--appearedSeagullNum == 0)
        {
            SetDelegate();
        }
    }

    public override void SetAppear(bool isAppear)
    {
        if(isAppear)
            _renderer.sprite = seagullSprite[Random.Range(0, seagullSprite.Length)];
        base.SetAppear(isAppear);
        if (!isAppear)
        {
            _disappearedTime = SystemManager.Instance.timer.GetGameTime;
        }
    }

    public override void MouseDown(Vector2 mousePos)
    {
        _coll.enabled = false;
        SystemManager.Instance.snack.GetSnack(this.transform);
    }

    private void SetDelegate()
    {
        if (_seagullInAction == null)
        {
            appearedSeagullNum = 0;
            foreach (UnityAction action in _seagullAllAction.GetInvocationList())
            {
                if (Random.Range(0, 99) < percentToAppearAnother)
                {
                    _seagullInAction += action;
                    appearedSeagullNum++;
                }
            }
            if (appearedSeagullNum == 0)
            {
                _seagullInAction += (UnityAction)_seagullAllAction.GetInvocationList()[Random.Range(0, _seagullAllAction.GetInvocationList().Length)];
                appearedSeagullNum = 1;
            }
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
        _seagullOutAction?.Invoke();
    }
}
