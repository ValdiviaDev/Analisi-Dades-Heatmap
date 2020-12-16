﻿using System.Collections;
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