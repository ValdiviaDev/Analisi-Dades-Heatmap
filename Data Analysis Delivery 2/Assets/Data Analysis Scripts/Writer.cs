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

    public static uint GetUUID()
    {
        return BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
    }

    public void AddDamageEvent(DamageEvent damage)
    {
        if(damage != null)
            Instance.damageList.Add(damage);
    }

    public static void Print(string s)
    {
        Instance._Print(s);
    }

    private void _Print(string s)
    {
        //if (File.Exists("damage.csv"))
        //{
        //    StreamWriter writer = File.AppendText("damage.csv");
        //    writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
        //    writer.Close();
        //}
        //else
        //{
        //    StreamWriter writer = File.CreateText("sessions.csv");
        //    writer.WriteLine("session_id;username;session_start;session_end");
        //    writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
        //    writer.Close();
        //}


      // if (File.Exists("damage.csv"))
      // {
      //     StreamWriter writer = File.AppendText("damage.csv");
      //
      //     foreach (DamageEvent damage in Instance.damageList)
      //     {
      //         writer.WriteLine(Instance.username + ";" + damage.crash_id.ToString("0000000000") + ";" + damage.position.x + ";" + crash.position.y + ";" + crash.position.z + ";" + crash.current_lap + ";" + crash.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + crash.session_id + ";" + crash.collision_obj_id);
      //     }
      //
      //     writer.Close();
      // }
      // else
      // {
      //     StreamWriter writer = File.CreateText("crashes.csv");
      //
      //     writer.WriteLine("username;crash_id;position_x;position_y;position_z;current_lap;time;session_id;collision_obj_id");
      //
      //     foreach (DamageEvent damage in Instance.damageList)
      //     {
      //         writer.WriteLine(Instance.username + ";" + crash.crash_id.ToString("0000000000") + ";" + crash.position.x + ";" + crash.position.y + ";" + crash.position.z + ";" + crash.current_lap + ";" + crash.time.ToString("dd/MM/yyyy hh:mm:ss") + ";" + crash.session_id + ";" + crash.collision_obj_id);
      //     }
      //     writer.Close();
      // }
    }


}
