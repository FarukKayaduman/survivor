using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private SceneAsset mainMenuScene;
    [SerializeField] private SceneAsset loadingScene;

    private void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(mainMenuScene.name, LoadSceneMode.Additive);
        
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;
            
            yield return null;
        }
        SceneManager.UnloadSceneAsync(loadingScene.name);
    }
}