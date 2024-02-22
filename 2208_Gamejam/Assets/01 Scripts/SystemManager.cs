using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class SystemManager : SingletonBehavior<SystemManager>
{
    public readonly static string CASTLE_TAG = "Castle";
    public readonly static string GET_BEST_SCORE = "bestScore";

    [SerializeField]private UIManager ui;
    public InputSystem input;
    public Timer timer;

    public Effect effect;

    [SerializeField]
    public Snack snack;

    public bool isGameOver = false;

    [SerializeField] private float bucketInvincibilityTime = 5f;
    [SerializeField] private int remainedBucket = 3;
    private int remainedTotalBucket;
    [SerializeField] private GameObject bucket;

    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject newBestText;

    [SerializeField] private GameObject deathCamParent;
    private GameObject[] _deathCams;

    [SerializeField]
    private CastleHitEffect castleHit;

    [SerializeField] private AudioClip castleHitSound;

    public Datas playData = new Datas();

    private AudioSource _audioSource;
    public int RemainedBucket
    {
        get => remainedBucket;
        set
        {
            remainedBucket = value;
            ui.SetBucketImage(remainedBucket);
        }
    }
    [SerializeField] private int remainedHP = 3;
    public int ReminedHP
    {
        get => remainedHP;
        set
        {
            remainedHP = value;
            _audioSource.clip = castleHitSound;
            _audioSource.Play();
            ui.SetHpImage(remainedHP);
            if (remainedHP <= 0)
            {
                isGameOver = true;
                GameOver();
            }
        }
    }

    private GameObject _castle;
    private Collider2D _castleColl;

    public Vector2 CastlePos { get; private set; }

    public bool IsUseBucket { get; private set; } = false;
    private float _bucketUseTime = 0;

    public void UseBucket()
    {
        if (remainedBucket > 0 && !IsUseBucket && Time.timeScale > 0)
        {
            IsUseBucket = true;
            RemainedBucket--;
            bucket.SetActive(true);
        }

    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    protected override void OnAwake()
    {
        GameManager.Instance.KeyAction += SetPause;
        GameManager.Instance.setting.UnmuteSFX();
        GameManager.Instance.setting.SetFirst();
        defaultPanel.SetActive(false);
        newBestText.SetActive(false);
        playData.GameStartNum = 1;
        Time.timeScale = 1;
        timer = new Timer(ui.SetTimerText);
        _castle = GameObject.FindGameObjectWithTag(CASTLE_TAG);
        _castleColl = _castle.GetComponent<Collider2D>();
        CastlePos = _castle.transform.position;
        remainedTotalBucket = remainedBucket;
        if (!bucket)
            bucket = _castle.transform.GetChild(0).gameObject;
        bucket.SetActive(false);
        if (!input)
            input = GetComponent<InputSystem>();
        _audioSource = GetComponent<AudioSource>();
        _deathCams = new GameObject[deathCamParent.transform.childCount];
        for (int i = 0; i < _deathCams.Length; i++)
            _deathCams[i] = deathCamParent.transform.GetChild(i).gameObject;
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

    public void SetPause()
    {
        defaultPanel.SetActive(true);
        pausePanel.SetActive(true);
        SetTimeScale(0);
        GameManager.Instance.setting.MuteSFX();
    }
    public void CastleHit(Vector2 hitPoint)
    {
        castleHit.HitEffect(hitPoint);
    }

    public void DisableAllDeathCams()
    {
        foreach (GameObject obj in _deathCams)
        {
            obj.SetActive(false);
        }
    }

    public void GameOver()
    {
        GameManager.Instance.setting.MuteSFX();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.parent.gameObject.SetActive(true);
        ui.SetGameOverNowScoreText(timer.GetGameTime);

        if (PlayerPrefs.GetFloat(GET_BEST_SCORE) < timer.GetGameTime)
        {
            newBestText.SetActive(true);
            PlayerPrefs.SetFloat(GET_BEST_SCORE, timer.GetGameTime);
        }
        ui.SetGameOverBestScoreText();

        playData.GameTotalPlayTime = timer.GetGameTime;
        playData.TotalUsedGuard = remainedTotalBucket - remainedBucket;

        GameManager.Instance.totalData += playData;
    }

    public void ChangeScene(string sceneName)
    {
        GameManager.Instance.KeyAction = null;
        GameManager.Instance.ChangeScene(sceneName);
    }

    public void ChangeScene(int sceneNum)
    {
        GameManager.Instance.KeyAction = null;
        GameManager.Instance.ChangeScene(sceneNum);
    }
}
