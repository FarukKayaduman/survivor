using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 2.25f; // Time between each enemy spawn

    private Transform _target; // Reference to the character's transform
    private float _timer; // Timer to control enemy spawning

    private void Start()
    {
        // Start the _timer
        _timer = _spawnInterval;

        // Find the character
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Decrement the _timer
        _timer -= Time.deltaTime;

        // Check if it's time to spawn an enemy
        if (_timer <= 0f)
        {
            // Spawn an enemy
            SpawnEnemy();

            // Reset the _timer
            _timer = _spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        // Determine a random position outside the screen
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the enemy at the spawn position
        GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

        // Set the enemy's _target as the character
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.SetTarget(_target);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Determine the screen bounds
        Camera mainCamera = Camera.main;
        float screenMinX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        float screenMaxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;
        float screenMinY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;
        float screenMaxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y;

        // Choose a random side of the screen to spawn the enemy
        int side = Random.Range(0, 4); // 0: top, 1: right, 2: bottom, 3: left

        // Calculate the spawn position based on the chosen side
        Vector3 spawnPosition = Vector3.zero;
        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector3(Random.Range(screenMinX, screenMaxX), screenMaxY + 1f, 0f);
                break;
            case 1: // Right
                spawnPosition = new Vector3(screenMaxX + 1f, Random.Range(screenMinY, screenMaxY), 0f);
                break;
            case 2: // Bottom
                spawnPosition = new Vector3(Random.Range(screenMinX, screenMaxX), screenMinY - 1f, 0f);
                break;
            case 3: // Left
                spawnPosition = new Vector3(screenMinX - 1f, Random.Range(screenMinY, screenMaxY), 0f);
                break;
        }

        return spawnPosition;
    }
}
