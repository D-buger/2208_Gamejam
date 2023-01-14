using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Texture2D defenseCursorTexture;
    [SerializeField] private Texture2D dragCursorTexture;

    [SerializeField]private Settings setting;

    public float CursorSize { get; private set; }
    public readonly Vector2 cursorHotspot = new Vector2(0.45f, 0f);

    public Datas totalData;

    protected override void OnAwake()
    {
        CursorSize = defaultCursorTexture.width;
        if (!setting)
            setting = GameObject.FindGameObjectWithTag("Settings")?.GetComponent<Settings>();
    }

    private void Start()
    {
        ChangeCursorToDefense(false);
        setting?.SetFirst();
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
            Cursor.SetCursor(defenseCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
        else
            Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToDrag(bool isCursorDrag)
    {
        if (isCursorDrag)
            Cursor.SetCursor(dragCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
        else
            Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }
}
