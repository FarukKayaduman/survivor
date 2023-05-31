using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletContainer;
    [SerializeField] private Rigidbody2D _rigidbody2D; // Players Rigidbody2D component

    private readonly float _shootingRange = 2.0f;
    private readonly float _bulletSpeed = 100.0f;
    private readonly float _moveSpeed = 2.0f; // Character movement speed
    private float _health = 100.0f;

    private Transform _target;
    private float _timer; // Timer to control shooting frequency

    public static float FireRate = 2.0f; // Time between each shot
    public static float BulletDamage = 10.0f;

    private void OnEnable()
    {
        EventManager.OnEnemyAttackEvent += TakeDamage;
    }

    private void Start()
    {
        _timer = FireRate;
        UpdateHealthInfo(_health);
    }

    private void Update()
    {
        FindNearestEnemy();

        _timer -= Time.deltaTime;

        // Check if it's time to shoot
        if (_timer <= 0f && _target != null)
        {
            Shoot();

            // Reset the _spawningTimer
            _timer = FireRate;
        }

        // Read the keyboard inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        _rigidbody2D.velocity = movement * _moveSpeed;
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
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Set the closest enemy as the _target
        _target = closestEnemy != null ? closestEnemy.transform : null;
    }

    private void Shoot()
    {
        // Instantiate a bullet and set its direction towards the _target
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
            targetDirection = transform.right;
            bulletQuaternion = Quaternion.Euler(transform.up - new Vector3(0, 0, 90f));
        }

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, bulletQuaternion, _bulletContainer);

        bullet.GetComponent<Rigidbody2D>().AddForce(targetDirection * _bulletSpeed);

        float bulletLifeTime = 5.0f;
        Destroy(bullet, bulletLifeTime);
    }

    private void UpdateHealthInfo(float health)
    {
        _health = health;
        EventManager.OnHealthUpdateEvent?.Invoke(health);
    }

    private void TakeDamage(float attackPoint)
    {
        float newHealth = _health - attackPoint;
        UpdateHealthInfo(newHealth);
        if (newHealth <= 0)
        {
            GameManager.Instance.LoseGame();
        }
    }
}