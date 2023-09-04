using ScriptableObjects.GameEvent;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "GameData")]
public class GameDataSO : ScriptableObject
{
    public GameEvent OnCurrentTimeChanged;
    public GameEvent OnGoldCountChanged;

    public int DefaultTime { get; set; } = 60;
    
    public int CurrentDefeatedEnemyCount { get; set; }

    private int _currentTime;
    public int CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            OnCurrentTimeChanged.Invoke();
        }
    }

    private int _currentGoldCount;
    public int CurrentGoldCount
    {
        get => _currentGoldCount;
        set
        {
            _currentGoldCount = value;
            OnGoldCountChanged.Invoke();
        }
    }

    private void Awake()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        CurrentTime = DefaultTime;
        CurrentDefeatedEnemyCount = 0;
        CurrentGoldCount = 0;
    }
}
