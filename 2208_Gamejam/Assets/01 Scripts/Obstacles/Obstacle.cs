using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleTag))]
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected GameObject deathCamera;

    [SerializeField] protected WarningSign warning;

    [SerializeField] protected PositionStorage appearPos;
    protected ObstacleData datas;

    [SerializeField] protected AudioClip enableSound;

    protected IDamgeSystem damage;
    protected Vector2 oriPos;

    protected bool isAppear = false;
    protected bool isPlay = true;

    protected float appearedTime = 0f;
    protected float disappearedTime = 0f;

    protected SpriteRenderer _renderer;
    protected Collider2D _coll;

    protected AudioSource audioSource;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _coll = gameObject.GetComponent<Collider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        oriPos = transform.position;
        OnAwke();
    }

    protected abstract void OnAwke(); 

    protected virtual void Start()
    {
        SetAppear(false);
    }

    protected virtual void Update()
    {
        if (!isAppear && SystemManager.Instance.timer.isTimePasses(disappearedTime + datas.delayAfterDamage, datas.appearTime.GetRandomFloat()))
        {
            appearedTime = SystemManager.Instance.timer.GetGameTime;
            if (!warning)
                SetAppear(true);
            else
                warning.WarningSignEnable();
        }
    }
    protected virtual void FixedUpdate()
    {
        if(isAppear && isPlay)
            Moving();
    }

    protected void PlayEnableSound()
    {
        if (enableSound)
        {
            audioSource.clip = enableSound;
            audioSource.Play();
        }
    }

    protected void SetAudioAndPlay(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    public abstract void Moving();

    public virtual void SetAppear(bool appear)
    {
        isAppear = appear;
        _renderer.enabled = appear;
        _coll.enabled = appear;

        if (!appearPos.GetRandomPosition().Equals(Vector2.zero) && appear == true)
            transform.position = appearPos.GetRandomPosition();

        if (!appear)
        {
            appearedTime = 0;
            disappearedTime = SystemManager.Instance.timer.GetGameTime;
            transform.position = oriPos;
        }
        else
        {
            PlayEnableSound();
        }
    }

    public abstract void MouseDown(Vector2 mousePos);
    public abstract void OnDrag(Vector2 mousePos);
    public abstract void MouseUp(Vector2 mousePos);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision);
    }

    protected virtual void OnEnter(Collider2D collision)
    {
        if (collision.CompareTag(SystemManager.CASTLE_TAG))
        {
            damage.DamageToPlayer(_coll.ClosestPoint(collision.gameObject.transform.position));
            AfterDamage();
        }
    }
    public abstract void AfterDamage();

}
