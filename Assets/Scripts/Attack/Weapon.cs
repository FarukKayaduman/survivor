using System.Collections;
using System.Collections.Generic;
using Characters;
using ObjectPool;
using UnityEngine;

namespace Attack
{
    public class Weapon : ObjectPool<Bullet>
    {
        private Transform _target;
        
        private bool _timeToShoot = true;
        private float _shootingTimer;

        private const int PoolDefaultCapacity = 10;
        private const int PoolMaxSize = 30;
        private const float ShootingRange = 2.0f;

        public static float FireRate = 2.0f;
        
        public static Weapon Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            SetPool(objectPrefab, PoolDefaultCapacity, PoolMaxSize);
        }
        
        public void PerformAttack()
        {
            if(!_timeToShoot)
                return;

            FindTargetEnemy();

            Quaternion bulletQuaternion;

            if (_target != null)
            {
                Vector2 targetDirection = (_target.transform.position - transform.position).normalized;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                bulletQuaternion = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                bulletQuaternion = Quaternion.Euler(transform.up - new Vector3(0, 0, 90f));
            }

            Bullet bullet = GetItem();
            Transform bulletTransform = bullet.transform;
            bulletTransform.position = transform.position;
            bulletTransform.rotation = bulletQuaternion;
            bullet.gameObject.SetActive(true);
            bullet.SetVelocity();

            StartCoroutine(DelayShooting());
        }

        private void FindTargetEnemy()
        {
            List<Enemy> enemies = EnemySpawner.Instance.ActivePool;

            float closestDistance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance >= closestDistance || distance > ShootingRange)
                    continue;
                
                closestDistance = distance;
                closestEnemy = enemy;
            }

            _target = closestEnemy != null ? closestEnemy.transform : null;
        }
        
        private IEnumerator DelayShooting()
        {
            _timeToShoot = false;
            yield return new WaitForSeconds(FireRate);
            _timeToShoot = true;
        }
        
    }
}
