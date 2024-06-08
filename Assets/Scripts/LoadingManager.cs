using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    
    private const string MainMenuScene = "MainMenuScene";
    private const string LoadingScene = "LoadingScene";

    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(MainMenuScene, LoadSceneMode.Additive);
        
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;
            
            yield return null;
        }
        SceneManager.UnloadSceneAsync(LoadingScene);
    }
}