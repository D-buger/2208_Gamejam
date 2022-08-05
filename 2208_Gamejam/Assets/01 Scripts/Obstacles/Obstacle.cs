using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleTag))]
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected TimeStorage _appearTime;
    [SerializeField] protected Vector2 _appearPos;

    protected IDamgeSystem _damage;

    protected bool _isAppear = false;
    public bool isPlay = true;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        if(_appearPos.Equals(Vector2.zero))
            _appearPos = transform.position;
        SetAppear(_isAppear);
    }

    protected virtual void Update()
    {
        if (!_isAppear && SystemManager.Instance.timer.isTimePasses(_appearTime.GetRandomTime()))
        {
            _isAppear = true;
            SetAppear(_isAppear);
        }
    }

    protected virtual void FixedUpdate()
    {
        if(_isAppear && isPlay)
            Moving();
    }

    public abstract void Moving();

    public virtual void SetAppear(bool isApear)
    {
        _renderer.enabled = isApear;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(SystemManager.CASTLE_TAG))
        {
            _damage.DamageToPlayer();
        }
    }
}
