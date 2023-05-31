using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        string gameSceneName = "GameScene";
        string uiSceneName = "InGameUI";
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);
    }
}
