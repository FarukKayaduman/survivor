using System.Collections;
using ScriptableObjects.GameEvent;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;

    [SerializeField] private GameEvent onPlayerWin;

    private bool _timerStarted;
    private float _passedTime;

    private Coroutine _timerCoroutine;

    [HideInInspector] public bool FirstStart = true;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        gameData.CurrentTime = gameData.DefaultTime;
        StartTimer();
    }

    private void Update()
    {
        if (gameData.CurrentTime > 0 || FirstStart)
            return;
        
        onPlayerWin.Invoke();
        StopGame();
    }

    private void StartTimer()
    {
        if(_timerStarted)
            return;

        _timerCoroutine = StartCoroutine(StartTimerCount());
        _timerStarted = true;
    }

    private IEnumerator StartTimerCount()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            gameData.CurrentTime--;
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    private void StopTimer()
    {
        if(_timerStarted)
            StopCoroutine(_timerCoroutine);
    }

    public void IncreaseGoldCount(int gettingGoldCount)
    {
        gameData.CurrentGoldCount += gettingGoldCount;
    }

    public void StopGame()
    {
        Time.timeScale = 0.0f;
        StopTimer();
    }
}
