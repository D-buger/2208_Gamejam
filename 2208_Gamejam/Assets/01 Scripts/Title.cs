using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOO;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject googlePlayPanel;
    private GameObject _loginFailed;
    private GameObject _loginSucceed;
    private GameObject _pleaseLogin;

    private bool _isLoginCheck = false;

    [SerializeField] private TMP_Text highScoreText;
    private Timer _timer;

    private void Start()
    {
        _pleaseLogin = googlePlayPanel.transform.GetChild(0).gameObject;
        _loginSucceed = googlePlayPanel.transform.GetChild(1).gameObject;
        _loginFailed = googlePlayPanel.transform.GetChild(2).gameObject;


        if (!PlayerPrefs.HasKey("StartOption"))
        {
            PlayerPrefs.SetInt("StartOption", 0);
            PlayerPrefs.SetInt(Settings.SFX_VOLUME, 5);
            PlayerPrefs.SetInt(Settings.BGM_VOLUME, 5);
        }

        if (PlayerPrefs.GetInt("StartOption") == 0)
        {
            GameManager.Instance.ChangeScene(1);
        }


        _timer = new Timer();
        highScoreText.text = Util.StringBuilder("최고기록\n" + PlayerPrefs.GetFloat(SystemManager.GET_BEST_SCORE).ToString("0.00"), "s");

    }

    private void Update()
    {
        if (GameManager.Instance.gsActivated && !_isLoginCheck && !GameManager.Instance.gsIsLogin)
        {
             _isLoginCheck = true;
             googlePlayPanel.SetActive(true);
             _pleaseLogin.SetActive(true);

        }

        _timer.TimeUpdate();
    }

    public void GooglePlayLogin()
    {
        GameManager.Instance.GoogleLogin();
        googlePlayPanel.SetActive(true);
        if (GameManager.Instance.gsIsLogin)
        {
            _loginSucceed.SetActive(true);
        }
        else
        {
            _loginFailed.SetActive(true);
        }
    }
}
