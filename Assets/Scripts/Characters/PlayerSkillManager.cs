using Attack;
using UnityEngine;

namespace Characters
{
    public class PlayerSkillManager : MonoBehaviour
    {
        [SerializeField] private GameDataSO gameData;

        public static int ShotFrequencyLevel;
        private static int _shotFrequencyMaxLevel = 10;
        public static int ShotFrequencyIncrementCost = 20;
        
        public static int ShovelLevel;
        private static int _shovelMaxLevel = 10;
        public static int ShovelIncrementCost = 30;

        public static PlayerSkillManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            ResetSkillsValues();
        }

        private static void ResetSkillsValues()
        {
            ShotFrequencyLevel = 0;
            _shotFrequencyMaxLevel = 10;
            ShotFrequencyIncrementCost = 20;

            ShovelLevel = 0;
            _shovelMaxLevel = 10;
            ShovelIncrementCost = 30;
        }

        public void UpgradeShotFrequencyAbility()
        {
            if (ShotFrequencyLevel >= _shotFrequencyMaxLevel || gameData.CurrentGoldCount < ShotFrequencyIncrementCost)
                return;
            
            float shotFrequencyIncrementAmount = 0.1f;
            Weapon.FireRate -= shotFrequencyIncrementAmount;
            GameManager.Instance.IncreaseGoldCount(-ShotFrequencyIncrementCost);
            ShotFrequencyLevel++;
            ShotFrequencyIncrementCost = (int)(ShotFrequencyIncrementCost * 1.1f);
        }
        
        public void UpgradeShovelAbility()
        {
            if (ShovelLevel >= _shovelMaxLevel || gameData.CurrentGoldCount < ShovelIncrementCost)
                return;
            
            ShovelsController.Instance.AddOneShovel();
            GameManager.Instance.IncreaseGoldCount(-ShovelIncrementCost);
            ShovelLevel++;
            ShovelIncrementCost = (int)(ShovelIncrementCost * 1.1f);
        }
    }
}
