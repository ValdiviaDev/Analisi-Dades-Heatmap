using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
{
    public static Writer Instance;
    private uint sessionID = 0u;

    private List<DamageEvent> damageList = new List<DamageEvent>();

    // Time
    private DateTime sessionStartTime;
    private DateTime sessionEndTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SessionStart()
    {
        Instance.sessionStartTime = DateTime.Now;
        Instance.sessionID = GetUUID();
    }

    public void SessionFinished()
    {
        Instance.sessionEndTime = DateTime.Now;
        Print(); //Serialize data
    }

    public static uint GetUUID()
    {
        return BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
    }

    public void AddDamageEvent(DamageEvent damage)
    {
        if(damage != null)
            Instance.damageList.Add(damage);
    }

    public static void Print(/*string s*/)
    {
        Instance._Print();
    }

    private void _Print(/*string s*/)
    {


       if (File.Exists("damage.csv"))
       {
           StreamWriter writer = File.AppendText("damage.csv");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
            }
      
           writer.Close();
       }
       else
       {
           StreamWriter writer = File.CreateText("crashes.csv");
      
           writer.WriteLine("session_id;position_x;position_y;position_z");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
               writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
           }
           writer.Close();
       }
    }


}
