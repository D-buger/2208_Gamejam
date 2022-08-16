using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private static readonly int _loadSceneNum = 2;

    public static string nextScene;

    [SerializeField] private Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());   
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(_loadSceneNum);
    }
    public static void LoadScene(int sceneNum)
    {
        string nextScenePath = SceneUtility.GetScenePathByBuildIndex(sceneNum);
        nextScene = nextScenePath.Substring(0, nextScenePath.Length - 6).Substring(nextScenePath.LastIndexOf('/') + 1);
        SceneManager.LoadScene(_loadSceneNum);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            
        }

    }
}
