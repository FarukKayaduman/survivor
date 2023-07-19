using ScriptableObjects.GameEvent;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvent onPlayerWin;
    private int _goldCount;
    private float _passedTime;
    private const float TimeGoal = 60.0f;

    public static int DefeatedEnemyCount;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if(_passedTime < TimeGoal)
        {
            _passedTime += Time.deltaTime;
            float leftTime = TimeGoal - _passedTime;
            UpdateTimeInfo(leftTime);
        }
        else
        {
            onPlayerWin.Raise();
            StopGame();
        }
    }

    public void IncreaseGoldCount(int gettingGoldCount)
    {
        _goldCount += gettingGoldCount;
        EventManager.OnGoldUpdateEvent?.Invoke(_goldCount);
    }

    private void UpdateTimeInfo(float leftTime)
    {
        EventManager.OnTimeUpdateEvent?.Invoke(leftTime);
    }

    public void StopGame()
    {
        Time.timeScale = 0.0f;
    }
}
