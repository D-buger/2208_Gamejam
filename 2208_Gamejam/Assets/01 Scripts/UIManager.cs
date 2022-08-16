using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SOO;

[System.Serializable]
public class UIManager
{
    [Space(20)]
    [Header("UI")]
    [SerializeField] private Image[] hpImage;
    [SerializeField] private Image[] bucketImage;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text gameOverNowScoreText;
    [SerializeField] private TMP_Text gameOverBestScoreText;
    [SerializeField] private SpriteRenderer castleRenderer;
    [SerializeField] private Sprite[] castleSprites = new Sprite[3];

    public void SetHpImage(int hp)
    {
        for(int i = 0; i < hpImage.Length; i++)
        {
            hpImage[i].enabled = i <= hp - 1;
        }

        castleRenderer.sprite = castleSprites[hp];
    }

    public void SetBucketImage(int bucket)
    {
        for (int i = 0; i < bucketImage.Length; i++)
        {
            bucketImage[i].enabled = i <= bucket - 1;
        }
    }

    public void SetTimerText(float time)
    {
        timerText.text = SetTimeText(time);
    }

    public void SetGameOverNowScoreText(float time)
    {
        gameOverNowScoreText.text = SetTimeText(time);
    }

    public void SetGameOverBestScoreText()
    {
        gameOverBestScoreText.text = SetTimeText(PlayerPrefs.GetFloat("bestScore"));
    }

    private string SetTimeText(float time)
        => Util.StringBuilder(time.ToString("0.00"), "s");
}
