using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy _enemyInfo;

    private Transform _target; // Reference to the _target transform (character)

    private float _attack;
    private float _health;
    private float _moveSpeed;

    private readonly float _attackDelay = 1.0f;
    private float _timer;

    private void Awake()
    {
        SetEnemyInfo();
    }

    private void Start()
    {
        _timer = _attackDelay;
    }

    private void Update()
    {
        // Check if the _target exists
        if (_target == null)
            return;

        _timer -= Time.deltaTime;

        // Calculate the direction towards the _target
        Vector3 direction = (_target.position - transform.position).normalized;

        float offset = 0.25f;
        float distanceBetweenTargetAndEnemy = Vector3.Distance(transform.position, _target.transform.position);
        if (distanceBetweenTargetAndEnemy > offset)
        {
            transform.Translate(_moveSpeed * Time.deltaTime * direction);
        }
        else
        {
            if (_timer <= 0f)
            {
                AttackToPlayer(_attack);
                _timer = _attackDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(PlayerController.BulletDamage);
            Destroy(collision.gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }

    private void SetEnemyInfo()
    {
        _attack = _enemyInfo.attack;
        _health = _enemyInfo.health;
        _moveSpeed = _enemyInfo.moveSpeed;
    }

    private void AttackToPlayer(float attackPoint)
    {
        EventManager.OnEnemyAttackEvent?.Invoke(attackPoint);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        if( _health <= 0)
        {
            Destroy(gameObject);

            int earnedGoldCount = (int)_enemyInfo.health;
            GameManager.Instance.UpdateGoldCount(earnedGoldCount);

            GameManager.DefeatedEnemyCount++;
        }
    }
}
