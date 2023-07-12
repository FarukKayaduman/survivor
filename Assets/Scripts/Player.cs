using System;
using HealthSystem;
using ScriptableObjects.Player;
using UnityEngine;
using WeaponSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private PlayerSO playerData;
    
    private readonly float _initialHealthValue;
    private readonly float _shootingRange;
    private readonly float _bulletSpeed;

    private Transform _target;
    private float _timer; // Timer to control shooting frequency

    public static float FireRate = 2.0f; // Time between each shot

    [SerializeField] private Weapon weapon;
    public Health health;

    public Player()
    {
        _initialHealthValue = 100.0f;
        _shootingRange = 2.0f;
        _bulletSpeed = 100.0f;
    }

    private void OnEnable()
    {
        if (health != null) return;
        health.InitializeHealth(_initialHealthValue);
        playerData.health = _initialHealthValue;

    }

    private void Start()
    {
        _timer = FireRate;
    }

    private void Update()
    {
        // FindNearestEnemy();
        //
        // _timer -= Time.deltaTime;
        
        if (_timer <= 0f && _target != null)
        {
            weapon.PerformAttack();
        }

        // Read the keyboard inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2D.velocity = movement * playerData.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            health.GetHit(enemy.enemyData.attack);
        }
    }

    public void UpdatePlayerHealthData()
    {
        playerData.health = health.CurrentHealth;
    }
}