using Attack;
using UnityEngine;

namespace Characters
{
    public class PlayerSkillManager : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;

        private bool _shotFrequencyUpgradeUnlocked;

        public static int ShotFrequencyLevel;
        public static int ShotFrequencyIncrementCost;

        public static PlayerSkillManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            ShotFrequencyIncrementCost = 20;
        }

        public void UpgradeShotFrequencyAbility()
        {
            if (ShotFrequencyLevel < 11 && _shotFrequencyUpgradeUnlocked)
            {
                float shotFrequencyIncrementAmount = 0.1f;
                Weapon.FireRate -= shotFrequencyIncrementAmount;
                GameManager.Instance.IncreaseGoldCount(-ShotFrequencyIncrementCost);
                ShotFrequencyLevel++;
                ShotFrequencyIncrementCost = (int)(ShotFrequencyIncrementCost * 1.1f);
                _shotFrequencyUpgradeUnlocked = false;
            }
        }

        public void CheckForSkillCosts()
        {
            // Skill 1: Shot frequency increment
            if(gameData.CurrentGoldCount > ShotFrequencyIncrementCost)
            {
                _shotFrequencyUpgradeUnlocked = true;
                EventManager.OnSkillAvailabilityUpdateEvent?.Invoke(true);
            }
        }
    }
}
