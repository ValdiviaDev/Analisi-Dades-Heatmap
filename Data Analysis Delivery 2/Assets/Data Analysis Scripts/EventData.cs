using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public uint eventID;
    public float seconds_since_start;

    //public string WriteJSON()
    //{
    //    string json = JsonUtility.ToJson(this);
    //    return json;
    //}
    //
    //public EventData GetJSON(string json)
    //{
    //    return JsonUtility.FromJson<EventData>(json); //TODO: This probably goes outside the class idk
    //}
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

public class TimeSpentEvent : EventData
{
    public float total_time_spent;
}

public class DestroyedCrateEvent : EventData
{
    public Vector3 position;
    //public float time_spent;
    public int crates_destroyed;
}

public class DoorEvent : EventData
{
    //public float time_spent;
    public int door_num;
}

public class AttackEvent : EventData
{
    public int attacks_num;
    public int enemies_killed;
}

public class HealingEvent : EventData
{
    //public float time_spent;
    public int hearts;
    public int health_num;
}