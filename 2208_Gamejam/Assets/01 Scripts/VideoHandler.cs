using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Update()
    {
        if (videoPlayer.frame > 0 && videoPlayer.isPlaying == false)
        {
            SkipVideo();
        }
    }
    
    public void SkipVideo()
    {
        PlayerPrefs.SetInt("StartOption", 1);
        GameManager.Instance.ChangeScene(0);
    }
}
