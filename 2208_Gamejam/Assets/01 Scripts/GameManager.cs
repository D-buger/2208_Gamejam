using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Texture2D defenseCursorTexture;

    protected override void OnAwake()
    {
        if (PlayerPrefs.GetInt("isPlayBefore") == 1)
        {
            PlayerPrefs.SetInt("isPlayBefore", 1);
            SceneManager.LoadScene(2);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        LoadingScene.LoadScene(sceneName);
    }
    public void ChangeScene(int sceneNum)
    {
        Time.timeScale = 1;
        LoadingScene.LoadScene(sceneNum);
    }

    public void ChangeCursorToDefense(bool isCursorDefense)
    {
        if(isCursorDefense)
            Cursor.SetCursor(defenseCursorTexture, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
