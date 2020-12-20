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

public class DestroyCrateEvent : EventData
{
    public Vector3 position;
    public int crates_destroyed;
}

public class DoorEvent : EventData
{
    public int door_num;
}

public class KillEvent : EventData
{
  
    public int enemies_killed;
}

public class HealingEvent : EventData
{
  
    public int health_num;
}