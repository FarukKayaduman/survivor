using Characters;
using UnityEngine;

namespace Attack
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2D;
    
        private const float BulletSpeed = 6.0f;

        public void SetVelocity()
        {
            rb2D.velocity = (transform.up * BulletSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent<Enemy>(out var enemy))
            {
                Weapon.Instance.ReleaseItem(this);
                gameObject.SetActive(false);
            
                if (enemy.CurrentHealth <= 0)
                    EnemySpawner.Instance.ReleaseItem(enemy);
            }
        }
    }
}
