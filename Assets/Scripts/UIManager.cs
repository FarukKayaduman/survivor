using System;
using ScriptableObjects.Player;
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

    [SerializeField] private PlayerSO playerData;
    
    private void OnEnable()
    {
        EventManager.OnTimeUpdateEvent += SetTimeText;
        EventManager.OnGoldUpdateEvent += SetGoldText;
        EventManager.OnSkillAvailabilityUpdateEvent += UpdateSkillsStatus;
    }

    private void OnDisable()
    {
        EventManager.OnTimeUpdateEvent -= SetTimeText;
        EventManager.OnGoldUpdateEvent -= SetGoldText;
        EventManager.OnSkillAvailabilityUpdateEvent -= UpdateSkillsStatus;
    }

    public void SetHealthSlider()
    {
        _healthSlider.value = playerData.health;
    }

    private void SetTimeText(float leftTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(leftTime);
        string formattedTime = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        _timeText.text = formattedTime;
    }

    private void SetGoldText(int goldCount)
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

    public void ActivateLosePanel()
    {
        _endGamePanel.SetActive(true);

        _deadImage.SetActive(true);
        _survivedImage.SetActive(false);
        
        _defeatedEnemyCountText.text = GameManager.DefeatedEnemyCount.ToString();
    }
    
    public void ActivateWinPanel()
    {
        _endGamePanel.SetActive(true);

        _deadImage.SetActive(false);
        _survivedImage.SetActive(true);
        
        _defeatedEnemyCountText.text = GameManager.DefeatedEnemyCount.ToString();
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
