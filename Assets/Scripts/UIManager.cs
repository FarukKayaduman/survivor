using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _defeatedEnemyCountText;

    [SerializeField] private GameObject _shotFrequencyArea;
    [SerializeField] private TextMeshProUGUI _shotFrequencyLevelText;
    [SerializeField] private TextMeshProUGUI _shotFrequencyCostText;

    [SerializeField] private GameObject _startGamePanel;

    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private GameObject _deadImage;
    [SerializeField] private GameObject _survivedImage;

    private void OnEnable()
    {
        EventManager.OnHealthUpdateEvent += UpdateHealthSlider;
        EventManager.OnTimeUpdateEvent += UpdateTimeText;
        EventManager.OnGoldUpdateEvent += UpdateGoldText;
        EventManager.OnSkillAvailabilityUpdateEvent += UpdateSkillsStatus;
        EventManager.OnEndGameEvent += EndGamePanel;
    }

    private void OnDisable()
    {
        EventManager.OnHealthUpdateEvent -= UpdateHealthSlider;
        EventManager.OnTimeUpdateEvent -= UpdateTimeText;
        EventManager.OnGoldUpdateEvent -= UpdateGoldText;
        EventManager.OnSkillAvailabilityUpdateEvent -= UpdateSkillsStatus;
        EventManager.OnEndGameEvent -= EndGamePanel;
    }

    private void UpdateHealthSlider(float health)
    {
        _healthSlider.value = health;
    }

    private void UpdateTimeText(float leftTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(leftTime);
        string formattedTime = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        _timeText.text = formattedTime;
    }

    private void UpdateGoldText(int goldCount)
    {
        _goldText.text = goldCount.ToString();
    }

    public void UpgradeShotFrequency()
    {
        PlayerSkillManager.Instance.UpgradeShotFrequencyAbility();

        _shotFrequencyLevelText.text = "Level: " + PlayerSkillManager.ShotFrequencyLevel.ToString();
        _shotFrequencyCostText.text = "Cost: " + PlayerSkillManager.ShotFrequencyIncrementCost.ToString();
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

    private void EndGamePanel(bool winStatus, int defeatedEnemyCount)
    {
        _endGamePanel.SetActive(true);

        _deadImage.SetActive(!winStatus);
        _survivedImage.SetActive(winStatus);
        
        _defeatedEnemyCountText.text = defeatedEnemyCount.ToString();
    }

    public void StartGame()
    {
        _startGamePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f; 
        SceneLoader.StartGame();
    }
}
