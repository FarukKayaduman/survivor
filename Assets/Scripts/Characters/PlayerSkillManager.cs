using Attack;
using UnityEngine;

namespace Characters
{
    public class PlayerSkillManager : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;

        public static int ShotFrequencyLevel;
        public static int ShotFrequencyIncrementCost = 20;

        public static PlayerSkillManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void UpgradeShotFrequencyAbility()
        {
            if (ShotFrequencyLevel >= 11 || gameData.CurrentGoldCount <= ShotFrequencyIncrementCost)
                return;
            
            float shotFrequencyIncrementAmount = 0.1f;
            Weapon.FireRate -= shotFrequencyIncrementAmount;
            GameManager.Instance.IncreaseGoldCount(-ShotFrequencyIncrementCost);
            ShotFrequencyLevel++;
            ShotFrequencyIncrementCost = (int)(ShotFrequencyIncrementCost * 1.1f);
        }
    }
}
