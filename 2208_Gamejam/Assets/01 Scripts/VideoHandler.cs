using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart") == 0)
        {
            GameManager.Instance.ChangeScene(1);
        }
    }


    private void Update()
    {
        if ((ulong)videoPlayer.frame >= videoPlayer.frameCount - 1)
        {
            GameManager.Instance.ChangeScene(1);
        }
    }

}
