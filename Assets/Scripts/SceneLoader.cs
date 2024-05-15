using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset mainMenuScene;
    [SerializeField] private SceneAsset gameScene;
    [SerializeField] private SceneAsset loadingScene;
    
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
        SceneManager.LoadScene(loadingScene.name, LoadSceneMode.Additive);
    }
    
    public IEnumerator LoadGameAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(gameScene.name, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        yield return new WaitUntil(() => !operation.isDone);
        
        operation.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(mainMenuScene.name);
    }
}