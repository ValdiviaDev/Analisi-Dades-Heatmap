using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public uint eventID;
    public float seconds_since_start;
}

public class DeathEvent : EventData
{
    public Vector3 position;
    public Vector3 eulerAngles;
}

public class SessionEvent : EventData
{
    string userName;
}

public class DamageEvent : EventData
{
    public Vector3 position;
}

public class DestroyedCrateEvent : EventData
{
    public Vector3 position;
    public int crates_destroyed;
}

public class DoorEvent : EventData
{
    public int door_num;
}

public class AttackEvent : EventData
{
    public int attacks_num;
    public int enemies_killed;
}

public class HealingEvent : EventData
{
    public int hearts;
    public int health_num;
}