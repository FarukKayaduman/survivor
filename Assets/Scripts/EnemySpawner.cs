using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 1f; // Time between each enemy spawn
    public float moveSpeed = 2f; // Speed at which enemies move towards the character

    private Transform target; // Reference to the character's transform
    private float timer; // Timer to control enemy spawning

    private void Start()
    {
        // Start the timer
        timer = spawnInterval;

        // Find the character
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Decrement the timer
        timer -= Time.deltaTime;

        // Check if it's time to spawn an enemy
        if (timer <= 0f)
        {
            // Spawn an enemy
            SpawnEnemy();

            // Reset the timer
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        // Determine a random position outside the screen
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the enemy at the spawn position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Set the enemy's target as the character
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.SetTarget(target);
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
