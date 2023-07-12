using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
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
        EventManager.OnGoldUpdateEvent += CheckForSkillCosts;

        ShotFrequencyIncrementCost = 20;
    }

    private void OnDisable()
    {
        EventManager.OnGoldUpdateEvent -= CheckForSkillCosts;
    }

    public void UpgradeShotFrequencyAbility()
    {
        if (ShotFrequencyLevel < 11 && _shotFrequencyUpgradeUnlocked)
        {
            float shotFrequencyIncrementAmount = 0.1f;
            Player.FireRate -= shotFrequencyIncrementAmount;
            GameManager.Instance.UpdateGoldCount(-ShotFrequencyIncrementCost);
            ShotFrequencyLevel++;
            ShotFrequencyIncrementCost = (int)(ShotFrequencyIncrementCost * 1.1f);
            _shotFrequencyUpgradeUnlocked = false;
        }
    }

    private void CheckForSkillCosts(int goldCount)
    {
        // Skill 1: Shot frequency increment
        if(goldCount > ShotFrequencyIncrementCost)
        {
            _shotFrequencyUpgradeUnlocked = true;
            EventManager.OnSkillAvailabilityUpdateEvent?.Invoke(true);
        }
    }
}
