using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string MainMenuScene = "MainMenuScene";
    private const string GameScene = "GameScene";
    private const string LoadingScene = "LoadingScene";

    public static SceneLoader Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadLoadingScene();
    }

    private void LoadLoadingScene()
    {
        SceneManager.LoadScene(LoadingScene, LoadSceneMode.Additive);
    }
    
    public IEnumerator LoadGameAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameScene, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        yield return new WaitUntil(() => !operation.isDone);
        
        operation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(MainMenuScene);
    }
}