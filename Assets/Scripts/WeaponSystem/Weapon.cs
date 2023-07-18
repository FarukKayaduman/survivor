using System.Collections;
using ObjectPool;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapon : ObjectPool<Bullet>
    {
        private bool _timeToShoot = true;
        private const float ShootingRange = 2.0f;
        private Transform _target;
        private float _timer; // Timer to control shooting frequency
        public static float FireRate = 2.0f; // Time between each shot

        private const int PoolDefaultCapacity = 10;
        private const int PoolMaxSize = 30;

        public static Weapon Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            SetPool(objectPrefab, PoolDefaultCapacity, PoolMaxSize);
        }
        
        public void PerformAttack()
        {
            if(!_timeToShoot) return;

            FindNearestEnemy();

            Quaternion bulletQuaternion;

            if (_target != null && Vector3.Distance(_target.transform.position, transform.position) <= ShootingRange)
            {
                Vector2 targetDirection = (_target.transform.position - transform.position).normalized;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                bulletQuaternion = Quaternion.Euler(new(0, 0, angle));
            }
            else
            {
                bulletQuaternion = Quaternion.Euler(transform.up - new Vector3(0, 0, 90f));
            }

            Bullet bullet = GetItem();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = bulletQuaternion;
            bullet.gameObject.SetActive(true);
            bullet.SetVelocity();

            _timeToShoot = false;
            StartCoroutine(DelayShooting());
        }

        private void FindNearestEnemy()
        {
            // Physics2D.OverlapCircle() (?)
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                // Check if the enemy is within shooting range and closer than the previous closest enemy
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            // Set the closest enemy as the _target
            _target = closestEnemy != null ? closestEnemy.transform : null;
        }
        
        private IEnumerator DelayShooting()
        {
            yield return new WaitForSeconds(FireRate);
            _timeToShoot = true;
        }
        
    }
}
