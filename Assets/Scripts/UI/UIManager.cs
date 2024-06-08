using System;
using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;
        
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _timeSlider;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _defeatedEnemyCountText;

        [SerializeField] private TextMeshProUGUI _shotFrequencyLevelText;
        [SerializeField] private TextMeshProUGUI _shotFrequencyCostText;
        
        [SerializeField] private TextMeshProUGUI _shovelLevelText;
        [SerializeField] private TextMeshProUGUI _shovelCostText;

        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private GameObject _deadImage;
        [SerializeField] private GameObject _survivedImage;

        [SerializeField] private CharacterSO playerData;

        private const string mainScene = "MainScene"; 
        
        private void Start()
        {
            GameManager.Instance.FirstStart = false;
            gameData.ResetValues();
            ResetSkillsInfo();
            SetTimeUI();
            ResetTimeSlider();
        }

        private void ResetTimeSlider()
        {
            _timeSlider.value = 0;
            _timeSlider.maxValue = gameData.DefaultTime;
        }

        private void ResetSkillsInfo()
        {
            _shotFrequencyLevelText.text = "Lvl " + PlayerSkillManager.ShotFrequencyLevel;
            _shotFrequencyCostText.text = $"{PlayerSkillManager.ShotFrequencyIncrementCost} <sprite index=2>";
            
            _shovelLevelText.text = "Lvl " + PlayerSkillManager.ShovelLevel;
            _shovelCostText.text = $"{PlayerSkillManager.ShovelIncrementCost} <sprite index=2>";
        }

        public void SetHealthSlider()
        {
            _healthSlider.value = playerData.currentHealth;
        }

        public void SetTimeUI()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameData.CurrentTime);
            string formattedTime = $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:00}";
            _timeSlider.value = gameData.DefaultTime - gameData.CurrentTime;
            _timeText.text = formattedTime;
        }

        public void SetGoldText()
        {
            _goldText.text = gameData.CurrentGoldCount.ToString();
        }

        public void UpgradeShotFrequency()
        {
            PlayerSkillManager.Instance.UpgradeShotFrequencyAbility();

            _shotFrequencyLevelText.text = "Lvl " + PlayerSkillManager.ShotFrequencyLevel;
            _shotFrequencyCostText.text = $"{PlayerSkillManager.ShotFrequencyIncrementCost} <sprite index=2>";
        }
        
        public void UpgradeShovel()
        {
            PlayerSkillManager.Instance.UpgradeShovelAbility();

            _shovelLevelText.text = "Lvl " + PlayerSkillManager.ShovelLevel;
            _shovelCostText.text = $"{PlayerSkillManager.ShovelIncrementCost} <sprite index=2>";
        }

        public void ActivateLosePanel()
        {
            _endGamePanel.SetActive(true);

            _deadImage.SetActive(true);
            _survivedImage.SetActive(false);
        
            _defeatedEnemyCountText.text = gameData.CurrentDefeatedEnemyCount.ToString();
        }
    
        public void ActivateWinPanel()
        {
            _endGamePanel.SetActive(true);

            _deadImage.SetActive(false);
            _survivedImage.SetActive(true);
        
            _defeatedEnemyCountText.text = gameData.CurrentDefeatedEnemyCount.ToString();
        }

        public void RestartGame()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(mainScene);
        }
    }
}
