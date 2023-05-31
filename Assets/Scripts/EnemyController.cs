using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemyInfo;

    private Transform _target; // Reference to the _target transform (character)

    private float _attack;
    private float _health;
    private float _moveSpeed;

    private void Awake()
    {
        SetEnemyInfo();
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
    
    public void SetTarget(Transform target)
    {
        this._target = target;
    }

    private void SetEnemyInfo()
    {
        _attack = enemyInfo.attack;
        _health = enemyInfo.health;
        _moveSpeed = enemyInfo.moveSpeed;
    }
}
