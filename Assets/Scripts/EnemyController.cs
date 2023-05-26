using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target; // Reference to the target transform (character)

    public float moveSpeed = 2f; // Speed at which the enemy moves towards the target

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        // Check if the target exists
        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Move towards the target
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("Bullet"))
        {
            // Remove the enemy from the scene
            Destroy(collision.gameObject);

            // Remove the bullet from the scene
            Destroy(gameObject);
        }
    }
}
