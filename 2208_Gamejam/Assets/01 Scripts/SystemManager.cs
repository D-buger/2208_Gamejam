using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SystemManager : SingletonBehavior<SystemManager>
{
    public readonly static string CASTLE_TAG = "Castle";
    public readonly static string GET_BEST_SCORE = "bestScore";

    [SerializeField]private UIManager _ui;
    public InputSystem input;
    public Timer timer;

    [SerializeField]
    public Snack snack;

    [SerializeField] private float bucketInvincibilityTime = 5f;
    [SerializeField] private int remainedBucket = 3;
    [SerializeField] private GameObject bucket;

    [SerializeField] private GameObject gameOverPanel;
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
            if (remainedHP <= 0)
                GameOver();
        }
    }

    private GameObject _castle;
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
        Time.timeScale = 1;
        timer = new Timer(_ui.SetTimerText);
        _castle = GameObject.FindGameObjectWithTag(CASTLE_TAG);
        CastlePos = _castle.transform.position;
        if (!bucket)
            bucket = _castle.transform.GetChild(0).gameObject;
        bucket.SetActive(false);
        if (!input)
            GetComponent<InputSystem>();
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

    public void GameOver()
    {
        Time.timeScale = 0;
        if (PlayerPrefs.GetFloat(GET_BEST_SCORE) < timer.GetGameTime)
        {
            gameOverPanel.transform.GetChild(0).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(GET_BEST_SCORE, timer.GetGameTime);
        }

        _ui.SetGameOverBestScoreText();
        _ui.SetGameOverNowScoreText(timer.GetGameTime);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.parent.gameObject.SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        GameManager.Instance.ChangeScene(sceneName);
    }

    public void ChangeScene(int sceneNum)
    {
        GameManager.Instance.ChangeScene(sceneNum);
    }
}
