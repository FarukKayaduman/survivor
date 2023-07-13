using HealthSystem;
using ScriptableObjects.Player;
using UnityEngine;
using WeaponSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private Weapon weapon;
    
    private readonly float _initialHealthValue = 100.0f;

    private Transform _target;
    private float _timer; // Timer to control shooting frequency

    public Health health;

    private void OnEnable()
    {
        if (health != null) return;
        health.InitializeHealth(_initialHealthValue);
        playerData.health = _initialHealthValue;

    }

    private void Update()
    {
        weapon.PerformAttack();

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