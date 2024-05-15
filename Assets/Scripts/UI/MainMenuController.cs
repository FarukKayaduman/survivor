using UnityEngine;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void StartGame()
        {
            StartCoroutine(SceneLoader.Instance.LoadGameAsync());
        }
    }
}
