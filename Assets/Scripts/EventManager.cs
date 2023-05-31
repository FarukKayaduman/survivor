using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<float> OnHealthUpdateEvent;
    public static Action<float> OnTimeUpdateEvent;
    public static Action<int> OnGoldUpdateEvent;

    public static Action<float> OnEnemyAttackEvent;

    public static Action<bool> OnSkillAvailabilityUpdateEvent;

    public static Action<bool, int> OnEndGameEvent;
}
