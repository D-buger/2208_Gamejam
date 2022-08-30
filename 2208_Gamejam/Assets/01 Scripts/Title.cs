using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOO;

public class Title : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    private Timer _timer;

    private void Start()
    {
        _timer = new Timer();
        highScoreText.text = Util.StringBuilder("최고기록\n" + PlayerPrefs.GetFloat(SystemManager.GET_BEST_SCORE).ToString("0.00"), "s");
    }

    private void Update()
    {
        _timer.TimeUpdate();
    }
}
