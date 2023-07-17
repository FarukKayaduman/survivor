using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private SceneAsset gameScene;
    [SerializeField] private SceneAsset inGameUI;
    [SerializeField] private SceneAsset loadingScene;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return null;

        AsyncOperation operation1 = SceneManager.LoadSceneAsync(gameScene.name, LoadSceneMode.Additive);
        AsyncOperation operation2 = SceneManager.LoadSceneAsync(inGameUI.name, LoadSceneMode.Additive);
        
        operation1.allowSceneActivation = false;
        operation2.allowSceneActivation = false;
        
        while (!(operation1.isDone || operation2.isDone))
        {
            loadingSlider.value = operation1.progress + operation2.progress;
            
            if (operation1.progress >= 0.9f && operation2.progress >= 0.9f)
            {
                operation1.allowSceneActivation = true;
                operation2.allowSceneActivation = true;
            }
            
            yield return null;
        }
        SceneManager.UnloadSceneAsync(loadingScene.name);
    }
}