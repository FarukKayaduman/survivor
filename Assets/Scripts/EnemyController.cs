using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _target; // Reference to the _target transform (character)

    [SerializeField] private float _moveSpeed = 1.0f; // Speed at which the enemy moves towards the _target

    public void SetTarget(Transform target)
    {
        this._target = target;
    }

    private void Update()
    {
        // Check if the _target exists
        if (_target != null)
        {
            // Calculate the direction towards the _target
            Vector3 direction = (_target.position - transform.position).normalized;

            // Move towards the _target
            transform.Translate(_moveSpeed * Time.deltaTime * direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
