using System;
using Characters;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;
        
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _defeatedEnemyCountText;

        [SerializeField] private GameObject _shotFrequencyArea;
        [SerializeField] private TextMeshProUGUI _shotFrequencyLevelText;
        [SerializeField] private TextMeshProUGUI _shotFrequencyCostText;

        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private GameObject _deadImage;
        [SerializeField] private GameObject _survivedImage;

        [SerializeField] private CharacterSO playerData;
        [SerializeField] private SceneAsset mainScene;
    
        private void OnEnable()
        {
            EventManager.OnSkillAvailabilityUpdateEvent += UpdateSkillsStatus;
        }

        private void OnDisable()
        {
            EventManager.OnSkillAvailabilityUpdateEvent -= UpdateSkillsStatus;
        }

        private void Start()
        {
            GameManager.Instance.FirstStart = false;
            gameData.ResetValues();
            SetTimeText();
        }

        public void SetHealthSlider()
        {
            _healthSlider.value = playerData.currentHealth;
        }

        public void SetTimeText()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameData.CurrentTime);
            string formattedTime = $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:00}";
            _timeText.text = formattedTime;
        }

        public void SetGoldText()
        {
            _goldText.text = gameData.CurrentGoldCount.ToString();
        }

        public void UpgradeShotFrequency()
        {
            PlayerSkillManager.Instance.UpgradeShotFrequencyAbility();

            _shotFrequencyLevelText.text = "Level: " + PlayerSkillManager.ShotFrequencyLevel;
            _shotFrequencyCostText.text = "Cost: " + PlayerSkillManager.ShotFrequencyIncrementCost;
        }

        private void UpdateSkillsStatus(bool shotFrequencyStatus)
        {
            switch (shotFrequencyStatus)
            {
                case true:
                    _shotFrequencyArea.SetActive(true);
                    break;
            }
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
            SceneManager.LoadScene(mainScene.name);
        }
    }
}
