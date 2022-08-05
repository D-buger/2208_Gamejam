using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleTag))]
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private TimeStorage _appearTime;
    [SerializeField] private Transform _appearPos;

    protected IDamgeSystem _damage;

    protected bool _isAppear = false;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
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
        if(_isAppear)
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
