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
        Instance.sessionID = GenerateUUID();
    }

    public void SessionFinished()
    {
        Instance.sessionEndTime = DateTime.Now;
        Print(); //Serialize data
    }

    public static uint GenerateUUID()
    {
        return BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
    }

    public void AddDamageEvent(DamageEvent damage)
    {
        if (damage != null)
        {
            damage.eventID = GenerateUUID();
            Instance.damageList.Add(damage);
        }
    }

    public static void Print(/*string s*/)
    {
        Instance._Print();
    }

    private void _Print(/*string s*/)
    {
        //Session data
        if (File.Exists("session_data.csv"))
        {
            StreamWriter writer = File.AppendText("session_data.csv");

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("session_data.csv");

            writer.WriteLine("session_id;time_session_start;time_session_end");

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

            writer.Close();
        }

        //Damage
        if (File.Exists("damage.csv"))
       {
           StreamWriter writer = File.AppendText("damage.csv");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.eventID.ToString("0000000000") + ";" + damage.seconds_since_start + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
            }
      
           writer.Close();
       }
       else
       {
           StreamWriter writer = File.CreateText("damage.csv");
      
           writer.WriteLine("session_id;event_id;seconds_since_start;position_x;position_y;position_z");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
               writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.eventID.ToString("0000000000") + ";" + damage.seconds_since_start + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
           }
           writer.Close();
       }
    }


}
