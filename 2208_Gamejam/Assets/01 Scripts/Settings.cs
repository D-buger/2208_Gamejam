using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private static readonly string SFX_VOLUME = "SFXVolume";
    private static readonly string BGM_VOLUME = "BGMVolume";
    private static readonly string SCREEN_TYPE = "IsFullScreen";

    [SerializeField] private GameObject bgmImage;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private GameObject sfxImage;
    [SerializeField] private Slider sfxVolume;

    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle borderlessScreen;

    public static void SetGameSettingsFirst()
    {
        Screen.fullScreenMode = (FullScreenMode)(PlayerPrefs.GetInt(SCREEN_TYPE));

    }

    private void OnEnable()
    {
        sfxVolume.value = PlayerPrefs.GetInt(SFX_VOLUME);
        SetImage(sfxImage, sfxVolume.value != 0);

        bgmVolume.value = PlayerPrefs.GetInt(BGM_VOLUME);
        SetImage(bgmImage, bgmVolume.value != 0);

        fullScreen.isOn = PlayerPrefs.GetInt(SCREEN_TYPE) == 1;
        borderlessScreen.isOn = !fullScreen.isOn;
    }


    public void SetSfxVolume()
    {
        int value = (int)sfxVolume.value;
        PlayerPrefs.SetInt(SFX_VOLUME, value);
        SetImage(sfxImage, value != 0);
    }

    public void SetBgmVolume()
    {
        int value = (int)bgmVolume.value;
        PlayerPrefs.SetInt(BGM_VOLUME, value);
        SetImage(bgmImage, value != 0);
    }

    private void SetImage(GameObject imageObj, bool isEnable)
    {
        imageObj.SetActive(isEnable);
    }

    public void SetScreenMode(bool isFullScreen)
    {
        Screen.fullScreenMode = (FullScreenMode)(isFullScreen ? 1 : 2);
        PlayerPrefs.SetInt(SCREEN_TYPE, isFullScreen ? 1 : 2);
    }
}
