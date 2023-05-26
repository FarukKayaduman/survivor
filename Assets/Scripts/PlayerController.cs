using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 2f; // Time between each shot
    public float shootingRange = 10f;
    public float moveSpeed = 5f; // Character movement speed

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Transform target;
    private float timer; // Timer to control shooting frequency

    private void Awake()
    {
        // Get the reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        timer = fireRate;
    }

    private void Update()
    {
        FindNearestEnemy();

        timer -= Time.deltaTime;

        // Check if it's time to shoot
        if (timer <= 0f && target != null)
        {
            Shoot();

            // Reset the timer
            timer = fireRate;
        }

        // Read the keyboard inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Apply movement to the character
        rb.velocity = movement * moveSpeed;
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // Check if the enemy is within shooting range and closer than the previous closest enemy
            if (distance <= shootingRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Set the closest enemy as the target
        target = closestEnemy != null ? closestEnemy.transform : null;
    }

    private void Shoot()
    {
        // Instantiate a bullet or projectile and set its direction towards the target
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(target);

        // Add additional logic here, like setting the bullet's speed or applying damage to the enemy
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward);
    }
}
