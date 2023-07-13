using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        private bool timeToShoot = true;
        private float _bulletSpeed = 80.0f; 
        private float _shootingRange = 2.0f;
        private Transform _target;
        private float _timer; // Timer to control shooting frequency
        public static float FireRate = 2.0f; // Time between each shot
        private float _bulletDamage = 10.0f;

        public void PerformAttack()
        {
            if(!timeToShoot) return;

            FindNearestEnemy();

            Vector2 targetDirection = transform.right.normalized;
            Debug.Log("1: " + targetDirection);
            Quaternion bulletQuaternion = Quaternion.Euler(transform.up - new Vector3(0, 0, 90f));

            
            if (_target != null && Vector3.Distance(_target.transform.position, transform.position) <= _shootingRange)
            {
                targetDirection = (_target.transform.position - transform.position).normalized;
                Debug.Log("2: " + targetDirection);

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                bulletQuaternion = Quaternion.Euler(new(0, 0, angle));
            }

            Instantiate(bulletPrefab, transform.position, bulletQuaternion);

            timeToShoot = false;
            StartCoroutine(DelayShooting());
        }
        
        public void FindNearestEnemy()
        {
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
            timeToShoot = true;
        }
        
    }
}
