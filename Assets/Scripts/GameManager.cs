using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _goldCount;
    private float _passedTime;
    private readonly float _timeGoal = 60.0f;

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
        if(_passedTime < _timeGoal)
        {
            _passedTime += Time.deltaTime;
            float leftTime = _timeGoal - _passedTime;
            UpdateTimeInfo(leftTime);
        }
        else
        {
            // Win status implementation
            StopGame();
        }
    }

    public void UpdateGoldCount(int gettingGoldCount)
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
