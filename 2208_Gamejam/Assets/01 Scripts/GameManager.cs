using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Texture2D defenseCursorTexture;
    [SerializeField] private Texture2D dragCursorTexture;

    public float CursorSize { get; private set; }
    public readonly Vector2 cursorHotspot = new Vector2(0.45f, 0f);

    public Settings setting;

    [HideInInspector] public Datas totalData;

    private GoogleService _gs;
    public bool gsActivated;
    public bool gsIsLogin => _gs.IsLogin;

    public TMPro.TMP_Text test;


    public UnityAction KeyAction;

    protected override void OnAwake()
    {
        CursorSize = defaultCursorTexture.width;
        _gs = new GoogleService();
        gsActivated = true;
        DataSave.LoadData(out totalData);

        if (!setting)
            setting = GameObject.FindGameObjectWithTag("Settings")?.GetComponent<Settings>();
    }

    private void OnApplicationPause(bool pause)
    {
        KeyAction?.Invoke();
    }

    private void Start()
    {
        ChangeCursorToDefense(false);
        setting?.SetFirst();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (KeyAction == null)
                Exit();
            else
                KeyAction.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            KeyAction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Menu))
        {
            KeyAction?.Invoke();
        }

        test.text = totalData.GameTotalPlayTime.ToString();
    }

    public void GoogleLogin()
    {
        _gs.Login();
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
