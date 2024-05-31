using Attack;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Weapon weapon;

        private Transform _target;
        private float _timer; // Timer to control shooting frequency

        private Vector2 _inputVector;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public static Player Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Update()
        {
            weapon.PerformAttack();
        }

        private void FixedUpdate()
        {
            // Read the keyboard inputs
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            _inputVector = new Vector2(inputX, inputY);
            rb2D.velocity = _inputVector * MoveSpeed;
        }
        
        private void LateUpdate()
        {
            animator.SetFloat(Speed, _inputVector.magnitude);
            
            if(_inputVector.x == 0)
                return;

            spriteRenderer.flipX = _inputVector.x < 0;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (transform.CompareTag("PlayerArea") && other.TryGetComponent<Enemy>(out var enemy))
            {
                GetHit(enemy.Damage);
            }
        }

        public void UpdatePlayerHealthData()
        {
            characterData.currentHealth = CurrentHealth;
        }
    }
}