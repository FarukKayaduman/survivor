using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<float> OnTimeUpdateEvent;
    public static Action<int> OnGoldUpdateEvent;

    public static Action<bool> OnSkillAvailabilityUpdateEvent;
}
