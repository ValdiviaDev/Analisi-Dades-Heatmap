using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class EventData : MonoBehaviour
//{
//    uint eventID;
//
//    public string WriteJSON()
//    {
//        string json = JsonUtility.ToJson(this);
//        return json;
//    }
//
//    public EventData GetJSON(string json)
//    {
//        return JsonUtility.FromJson<EventData>(json); //TODO: This probably goes outside the class idk
//    }
//}

public class DeathEvent
{
    Vector3 position;
    Vector3 eulerAngles;
}

public class SessionEvent
{
    string userName;
}

public class DamageEvent
{
    public Vector3 position;

}

public class TimeSpentEvent
{
    public float total_time_spent;
}

public class DestroyedCrateEvent
{
    public Vector3 position;
    public float time_spent;
    public int crates_destroyed;
}

public class DoorEvent
{
    public float time_spent;
    public int door_num;
}

public class AttackEvent
{
    public int attacks_num;
    public int enemies_killed;
}

public class HealingEvent
{
    public float time_spent;
    public int hearts;
    public int health_num;
}