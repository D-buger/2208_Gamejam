using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Seagull : Obstacle
{
    private static event UnityAction _seagullOutAction;
    private static event UnityAction _seagullInAction;
    private static event UnityAction _seagullAllAction;

    private static int appearedSeagullNum = 0;

    [SerializeField] private Sprite[] seagullSprite;
    [SerializeField] private Sprite seagullPassSprite;

    [SerializeField]
    private SeagullData data;

    [SerializeField] private AudioClip seagullSatisfiedSound;

    private Vector2 _target;

    private static float _appearedTime;
    private static float _disappearedTime;

    private bool _isGoBackOri = false;
    private bool _isFadeOut = false;

    private Animator effectAnimator;
    protected override void OnAwke()
    {
        _seagullOutAction = null;
        _seagullAllAction = null;
        _seagullInAction = null;
        datas = data;
        effectAnimator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        _seagullOutAction += () => 
        {
            if (isAppear)
            {
                _isGoBackOri = true;
                _coll.enabled = false;
            }
        };

        damage = new JustDamage(()=> 
        { 
            deathCamera.SetActive(true);

        });
        _target = SystemManager.Instance.CastlePos;

        _seagullAllAction += () => this.SetAppear(true);

        SetDelegate();
    }

    protected override void Update()
    {
        if (_seagullInAction != null && !isAppear && SystemManager.Instance.timer.isTimePasses(_disappearedTime + data.delayAfterDamage, data.appearTime.GetRandomFloat()))
        {
            _seagullInAction?.Invoke();
            _seagullInAction = null;

            _appearedTime = SystemManager.Instance.timer.GetGameTime;
        }
    }

    private float _distPerc = 0;
    private float _distColor = 0;
    public override void Moving()
    {
        if (_isGoBackOri)
        {
            _distPerc += Time.deltaTime;
            if(Vector2.Distance(transform.position, oriPos) <= 0.5f)
            {
                _isGoBackOri = false;
                _isFadeOut = true;
                _distPerc = 0;
            }
            transform.position = Vector2.Lerp(transform.position, oriPos, _distPerc);
        }
        else if (_isFadeOut)
        {
            _distColor += Time.deltaTime;
            if(_renderer.color.a < 0.05f)
            {
                _isFadeOut = false;
                _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 255);
                _distColor = 0;
                SetAppear(false);
                if (--appearedSeagullNum == 0)
                {
                    SetDelegate();
                }
            }
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, Mathf.Lerp(1, 0, _distColor));
        }
        else if(SystemManager.Instance.timer.GetGameTime - _appearedTime > data.moveTimeAfterAppear)
            transform.position = Vector3.MoveTowards(transform.position, _target, data.speed * Time.deltaTime);
    }

    public void SetDisable()
    {
        effectAnimator.SetTrigger("effectTrigger");
        SetAudioAndPlay(seagullSatisfiedSound);
        _renderer.color = Color.white;
        _renderer.sprite = seagullPassSprite;
        _coll.enabled = false;
        _isFadeOut = true;
    }

    public override void SetAppear(bool isAppear)
    {
        if(isAppear)
            _renderer.sprite = seagullSprite[Random.Range(0, seagullSprite.Length)];

        _renderer.color = Color.white;
        base.SetAppear(isAppear);
        oriPos = transform.position;
        if (!isAppear)
        {
            _disappearedTime = SystemManager.Instance.timer.GetGameTime;
        }
    }

    public override void MouseDown(Vector2 mousePos)
    {
        SystemManager.Instance.playData.SeagullTouchNum += 1;
        _renderer.color = Color.red;
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
                if (Random.Range(0, 99) < data.percentOfAppearAnother)
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
