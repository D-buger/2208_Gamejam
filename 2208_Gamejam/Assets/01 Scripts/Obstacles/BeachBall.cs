using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOO;

public class BeachBall : Obstacle
{
    private static bool isAppearOne = false;

    [SerializeField]
    private Sprite[] beachballSprite;
    [SerializeField]
    private Transform curveTrans;
    private Vector2[] curvePointVec;
    [SerializeField]
    private int point;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float draggedLimitTime = 1f;

    [SerializeField]
    private float disappearTimeAfterDrag = 3f;

    private Vector2[] _curveVec;

    private bool _isBounce = false;
    private Vector2 _bounceVec = Vector2.zero;

    protected override void OnAwke()
    {
        isAppearOne = false;
        _isBounce = false;
    }

    protected override void Start()
    {
        base.Start();
        damage = new JustDamage(() =>
        {
            _renderer.enabled = true;
            deathCamera.SetActive(true);
        });
        curvePointVec = new Vector2[3];

        curvePointVec[0] = transform.position;
        curvePointVec[1] = curveTrans.position;
        curvePointVec[2] = SystemManager.Instance.CastlePos;

        _curveVec = Util.CurvePointsOfVectors(point, curvePointVec);

        warning.beforeSignEnable += () => isAppearOne = true;
        warning.afterSignDisable += () => SetAppear(true);
    }

    protected override void Update()
    {
        if (!isAppearOne)
        {
            base.Update();
        }
    }

    private float _time = 0;
    public override void Moving()
    {
        _time += Time.deltaTime * speed;
        if (!_isBounce)
        {
            transform.position = _curveVec[Mathf.Clamp((int)_time, 0, point)];
        }
        else
        {
            if (_time > disappearTimeAfterDrag)
                SetAppear(false);
            else
            {
                transform.Translate(_bounceVec * Time.deltaTime * speed * 1.5f);
            }
        }
    }

    public override void SetAppear(bool isApear)
    {
        _renderer.sprite = beachballSprite[Random.Range(0, beachballSprite.Length)];
        _time = 0;
        _isBounce = false;
        _bounceVec = Vector2.zero;
        base.SetAppear(isApear);
    }


    public override void MouseDown(Vector2 mousePos)
    {

    }
    public override void OnDrag(Vector2 mousePos)
    {
        GameManager.Instance.ChangeCursorToDrag(true);
        if (SystemManager.Instance.input.DraggedTime < draggedLimitTime && !_isBounce && !SystemManager.Instance.input.DraggedPos.Equals(Vector2.zero))
        {
            _isBounce = true;
            _bounceVec = SystemManager.Instance.input.DraggedPos;
            _time = 0;
            isAppearOne = false;
        }
    }


    public override void MouseUp(Vector2 mousePos)
    {
    }

    public override void AfterDamage()
    {
        if(!SystemManager.Instance.isGameOver)
            SetAppear(false);
        isAppearOne = false;
    }
}
