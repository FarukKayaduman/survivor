using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Rigidbody2D _rigidbody2D; // Players Rigidbody2D component
    [SerializeField] private Slider _slider; // Reloading indicator

    private readonly float _fireRate = 2.0f; // Time between each shot
    private readonly float _shootingRange = 2.0f;
    private readonly float _bulletSpeed = 100.0f;
    private readonly float _moveSpeed = 2.0f; // Character movement speed

    private Transform _target;
    private float _timer; // Timer to control shooting frequency

    private void Start()
    {
        _timer = _fireRate;
        _slider.maxValue = _fireRate;
    }

    private void Update()
    {
        FindNearestEnemy();

        _timer -= Time.deltaTime;
        _slider.value = _fireRate - _timer;

        // Check if it's time to shoot
        if (_timer <= 0f && _target != null)
        {
            Shoot();

            // Reset the _timer
            _timer = _fireRate;
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

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, bulletQuaternion);

        bullet.GetComponent<Rigidbody2D>().AddForce(targetDirection * _bulletSpeed);
    }
}
