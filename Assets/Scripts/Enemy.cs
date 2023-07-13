using HealthSystem;
using ScriptableObjects.Enemy;
using ScriptableObjects.Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Health health;

    private Transform _target; // Reference to the _target transform (character)

    [HideInInspector] public EnemySO enemyData;

    private float _attack;
    private float _initialHealthValue;
    private float _moveSpeed;

    private readonly float _attackDelay = 1.0f;
    private float _timer;

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
            health.GetHit(playerData.defaultWeaponDamage);
            Destroy(collision.gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }

    public void SetEnemyInfo()
    {
        _attack = enemyData.attack;
        _initialHealthValue = enemyData.health;
        _moveSpeed = enemyData.moveSpeed;
        
        health.InitializeHealth(_initialHealthValue);
        spriteRenderer.sprite = enemyData.enemySprite;
    }

    private void AttackToPlayer(float attackPoint)
    {
        if (_target.TryGetComponent<Player>(out var player))
        {
            player.health.GetHit(attackPoint);
        }
    }

    public void Death()
    {
        Destroy(gameObject);

        int earnedGoldCount = (int)enemyData.health;
        GameManager.Instance.UpdateGoldCount(earnedGoldCount);

        GameManager.DefeatedEnemyCount++;
    }
}
