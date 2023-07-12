using ScriptableObjects.Enemy;
using ScriptableObjects.GameEvent;
using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour, IHittable
    {
        [field: SerializeField] public float CurrentHealth { get; private set; }
        
        [SerializeField] private GameEvent onDeath;
        [SerializeField] private GameEvent onHit;
        
        public void InitializeHealth(float startingHealth)
        {
            if (startingHealth < 0)
                startingHealth = 0;
            this.CurrentHealth = startingHealth;
        }

        public void GetHit(float damageValue)
        {
            CurrentHealth -= damageValue;

            if (CurrentHealth <= 0)
            {
                onDeath.Raise();
            }
            else
            {
                onHit.Raise();
            }
        }
    }
}
