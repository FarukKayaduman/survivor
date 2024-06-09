using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;

        [SerializeField] private CharacterSO finnPlayerData;
        [SerializeField] private CharacterSO ninaPlayerData;

        [SerializeField] private Image finnImage;
        [SerializeField] private Image ninaImage;

        private Color32 _selectedColor;
        
        private void Start()
        {
            _selectedColor = new Color32(0xB7, 0xB4, 0xE4, 255);
            
            if (gameData.SelectedCharacter.name == finnPlayerData.name)
                SelectFinn();
            else if (gameData.SelectedCharacter.name == ninaPlayerData.name)
                SelectNina();
        }

        public void StartGame()
        {
            StartCoroutine(SceneLoader.Instance.LoadGameAsync());
        }

        public void SelectFinn()
        {
            gameData.SelectedCharacter = finnPlayerData;
            finnImage.color = _selectedColor;
            ninaImage.color = Color.white;
        }

        public void SelectNina()
        {
            gameData.SelectedCharacter = ninaPlayerData;
            ninaImage.color = _selectedColor;
            finnImage.color = Color.white;
        }
    }
}
