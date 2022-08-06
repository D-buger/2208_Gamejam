using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SystemManager : SingletonBehavior<SystemManager>
{
    public readonly static string CASTLE_TAG = "Castle";

    [SerializeField]private UIManager _ui;
    public Timer timer;

    [SerializeField] private float bucketInvincibilityTime = 5f;
    [SerializeField] private int remainedBucket = 3;
    [SerializeField] private GameObject bucket;
    public int RemainedBucket
    {
        get => remainedBucket;
        set
        {
            remainedBucket = value;
            _ui.SetBucketImage(remainedBucket);
        }
    }
    [SerializeField] private int remainedHP = 3;
    public int ReminedHP
    {
        get => remainedHP;
        set
        {
            remainedHP = value;
            _ui.SetHpImage(remainedHP);
        }
    }

    private GameObject _castle;
    public Vector2 CastlePos { get; private set; }

    public bool IsUseBucket { get; private set; } = false;
    private float _bucketUseTime = 0;

    public void UseBucket()
    {
        if (remainedBucket > 0 && !IsUseBucket)
        {
            IsUseBucket = true;
            RemainedBucket--;
            bucket.SetActive(true);
        }

    }
    protected override void OnAwake()
    {
        timer = new Timer(_ui.SetTimerText);
        _castle = GameObject.FindGameObjectWithTag(CASTLE_TAG);
        CastlePos = _castle.transform.position;
        if (!bucket)
            bucket = _castle.transform.GetChild(0).gameObject;
        bucket.SetActive(false);
    }

    private void Update()
    {
        timer.TimeUpdate();
        if (IsUseBucket)
        {
            _bucketUseTime += Time.deltaTime;
            if (_bucketUseTime > bucketInvincibilityTime)
            {
                IsUseBucket = false;
                bucket.SetActive(false);
                _bucketUseTime = 0f;
            }
        }
    }

    
}
