using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public class EnemySpawner : ObjectPool<Enemy>
    {
        [SerializeField] private List<CharacterSO> enemyData;

        [SerializeField] private Transform target;

        private const int PoolDefaultCapacity = 100;

        private float _currentEnemyHealth;
    
        private const float InitialSpawnInterval = 1.0f;
        private float _currentSpawnInterval;

        private const float DifficultyIncreaseInterval = 10.0f;

        public static EnemySpawner Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            SetPool(objectPrefab, PoolDefaultCapacity);

            _currentSpawnInterval = InitialSpawnInterval;
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
            StartCoroutine(IncreaseSpawnIntervalCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            // TODO: Refactor this when using state machine
            while (true)
            {
                yield return new WaitForSeconds(_currentSpawnInterval);
                SpawnEnemy();
            }
        }
    
        private IEnumerator IncreaseSpawnIntervalCoroutine()
        {
            float difficultyIncreaseMultiplier = 0.1f;
        
            while (true)
            {
                yield return new WaitForSeconds(DifficultyIncreaseInterval);
            
                foreach (CharacterSO enemy in enemyData)
                {
                    enemy.currentHealth *= (1 + difficultyIncreaseMultiplier);
                }
            
                _currentSpawnInterval *= (1 - difficultyIncreaseMultiplier);
            }
        }
    
        public void SpawnEnemy()
        {
            int randomIndex = Random.Range(0, enemyData.Count);
        
            // Instantiate the enemy at the spawn position
            Enemy enemy = GetItem();
            enemy.transform.position = GetRandomSpawnPosition();
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetTarget(target);
            enemy.SetCharacterInfo(enemyData[randomIndex]);
            enemy.gameObject.SetActive(true);
        }

        private static Vector3 GetRandomSpawnPosition()
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
}
