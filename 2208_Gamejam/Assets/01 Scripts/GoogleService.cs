using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleService
{
    private bool isLogin;
    public bool IsLogin
    {
        get => isLogin;
        set => isLogin = value;
    }

    public GoogleService()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();
    }

    public void Login()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    IsLogin = true;
                else
                    IsLogin = false;

            });
        }
    }
}
