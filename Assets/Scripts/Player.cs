using ScriptableObjects.Character;
using UnityEngine;
using WeaponSystem;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Weapon weapon;

    private Transform _target;
    private float _timer; // Timer to control shooting frequency

    private void Update()
    {
        weapon.PerformAttack();
    }

    private void FixedUpdate()
    {
        // Read the keyboard inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2D.velocity = movement * MoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            GetHit(enemy.Damage);
        }
    }

    public void UpdatePlayerHealthData()
    {
        characterData.currentHealth = CurrentHealth;
    }
}