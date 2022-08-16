using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SOO;

public class Title : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        highScoreText.text = Util.StringBuilder("최고기록\n" + PlayerPrefs.GetFloat(SystemManager.GET_BEST_SCORE).ToString("0.00"), "s");
    }
}
