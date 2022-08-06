using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleTag))]
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected FloatStorage appearTime;
    [SerializeField] protected PositionStorage appearPos; 

    protected IDamgeSystem _damage;
    protected Vector2 oriPos;

    protected bool isAppear = false;
    protected bool isPlay = true;

    protected float appearedTime = 0f;

    private SpriteRenderer _renderer;
    private Collider2D _coll;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _coll = gameObject.GetComponent<Collider2D>();
        oriPos = transform.position;
        SetAppear(isAppear);
    }

    protected virtual void Update()
    {
        if (!isAppear && SystemManager.Instance.timer.isTimePasses(appearTime.GetRandomFloat()))
        {
            isAppear = true;
            appearedTime = SystemManager.Instance.timer.GetGameTime;
            SetAppear(isAppear);
        }
    }

    protected virtual void FixedUpdate()
    {
        if(isAppear && isPlay)
            Moving();
    }

    public abstract void Moving();

    public virtual void SetAppear(bool isApear)
    {
        if (!appearPos.GetRandomPosition().Equals(Vector2.zero) && isApear == true)
            transform.position = appearPos.GetRandomPosition();
        _renderer.enabled = isApear;
        _coll.enabled = isApear;

        if (!isAppear)
        {
            appearedTime = 0;
        }
    }

    public abstract void MouseDown(Vector2 mousePos);
    public abstract void OnDrag(Vector2 mousePos);
    public abstract void MouseUp(Vector2 mousePos);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(SystemManager.CASTLE_TAG))
        {
            _damage.DamageToPlayer();
            AfterDamage();
        }
    }

    public abstract void AfterDamage();
}
