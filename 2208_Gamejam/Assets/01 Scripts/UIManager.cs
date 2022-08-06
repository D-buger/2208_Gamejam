using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UIManager
{
    [Space(20)]
    [Header("UI")]
    [SerializeField] private Image[] hpImage;
    [SerializeField] private Image[] bucketImage;
    [SerializeField] private TMP_Text timerText;

    public void SetHpImage(int hp)
    {
        for(int i = 0; i < hpImage.Length; i++)
        {
            hpImage[i].enabled = i <= hp - 1;
        }
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
        timerText.text = time.ToString("0.00");
    }
}
