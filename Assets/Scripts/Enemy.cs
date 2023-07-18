using ScriptableObjects.Character;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private CharacterSO playerData;

    private Transform _target; // Reference to the _target transform (character)
    
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
            transform.Translate(MoveSpeed * Time.deltaTime * direction);
        }
        else
        {
            if (_timer <= 0f)
            {
                AttackToPlayer(Damage);
                _timer = _attackDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var bullet))
        {
            GetHit(playerData.defaultDamage);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void AttackToPlayer(float attackPoint)
    {
        if (_target.TryGetComponent<Player>(out var player))
        {
            player.GetHit(attackPoint);
        }
    }

    protected override void OnDeath()
    {
        EnemySpawner.Instance.ReleaseItem(this);
        gameObject.SetActive(false);

        int earnedGoldCount = (int)characterData.currentHealth;
        GameManager.Instance.IncreaseGoldCount(earnedGoldCount);

        GameManager.DefeatedEnemyCount++;
    }
}
