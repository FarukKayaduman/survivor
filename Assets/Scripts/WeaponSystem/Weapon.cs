using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        private bool _shootingDelayed;
        private readonly float _bulletSpeed; 
        private readonly float _shootingRange;
        private Transform _target;
        private float _timer; // Timer to control shooting frequency
        public static float FireRate = 2.0f; // Time between each shot
        private float BulletDamage = 10.0f;

        public void PerformAttack()
        {
            if(!_shootingDelayed) return;

            FindNearestEnemy();
            
            _shootingDelayed = true;
            
            Vector2 targetDirection;
            Quaternion bulletQuaternion;

            if (Vector3.Distance(_target.transform.position, transform.position) <= _shootingRange)
            {
                targetDirection = _target.transform.position - transform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
                bulletQuaternion = Quaternion.Euler(new(0, 0, angle));
            }
            else
            {
                var transform1 = transform;
                targetDirection = transform1.right;
                bulletQuaternion = Quaternion.Euler(transform1.up - new Vector3(0, 0, 90f));
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletQuaternion);

            bullet.GetComponent<Rigidbody2D>().AddForce(targetDirection * _bulletSpeed);

            float bulletLifeTime = 5.0f;
            Destroy(bullet, bulletLifeTime);

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
            _shootingDelayed = false;
        }
        
    }
}
