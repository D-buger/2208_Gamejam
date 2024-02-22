using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public static class DataSave
{
    private static string json;
    private static ISavedGameClient SavedGame() => PlayGamesPlatform.Instance.SavedGame;
    public static void LoadData(out Datas data)
    {
        if (GameManager.Instance.gsIsLogin)
        {
            string id = Social.localUser.id;
            string fileName = string.Format("{0}_data", id);

            SavedGame().OpenWithAutomaticConflictResolution(fileName,
                DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);

            data = JsonUtility.FromJson<Datas>(json);
        }
        else
        {
            Debug.Log("Load 실패 - login error");
            data = new Datas();
        }
    }

    public static void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadDataResult);
    }

    public static void LoadDataResult(SavedGameRequestStatus status, byte[] LoadedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            json = System.Text.Encoding.UTF8.GetString(LoadedData);

            Debug.Log("로드 성공");
        }
        else
        {
            Debug.Log("로드 실패");
        }
    }


    public static void SaveData(this Datas data)
    {
        if (GameManager.Instance.gsIsLogin)
        {
            json = JsonUtility.ToJson(data, true);

            string id = Social.localUser.id;
            string fileName = string.Format("{0}_data", id);


            SavedGame().OpenWithAutomaticConflictResolution(fileName,
                DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGame);
        }
        else
        {
            Debug.Log("Save 실패 - login error");
        }
    }

    public static void SaveGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            SavedGame().CommitUpdate(game, update, bytes, SaveDataResult);
        }
    }

    public static void SaveDataResult(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("세이브 성공");
        }
        else
        {
            Debug.Log("세이브 실패");
        }
    }

}
