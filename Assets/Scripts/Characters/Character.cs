using ScriptableObjects.GameEvent;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour, IHittable
    {
        protected internal float CurrentHealth { get; private set; }
        protected internal float Damage;
        protected float MoveSpeed;

        [SerializeField] private GameEvent onDeath;
        [SerializeField] private GameEvent onHit;
        [SerializeField] protected internal CharacterSO characterData;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;

        private void Start()
        {
            SetCharacterInfo(characterData);
        }

        public void SetCharacterInfo(CharacterSO characterSO)
        {
            characterData = characterSO;
            characterData.currentHealth = characterData.defaultHealth;
            CurrentHealth = characterData.defaultHealth;
            Damage = characterData.defaultDamage;
            MoveSpeed = characterData.moveSpeed;
            spriteRenderer.sprite = characterData.sprite;
            animator.runtimeAnimatorController = characterSO.animatorOverrideController;
        }

        public void GetHit(float damageValue)
        {
            CurrentHealth -= damageValue;

            if (CurrentHealth <= 0)
            {
                OnDeath();
            }
            else
            {
                OnHit();
            }
        }

        protected virtual void OnDeath()
        {
            onDeath.Invoke();
        }

        private void OnHit()
        {
            onHit.Invoke();
        }
    }
}