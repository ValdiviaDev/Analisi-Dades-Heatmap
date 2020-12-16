using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
{
    public static Writer Instance;
    private uint sessionID = 0u;

    private List<DamageEvent> damageList = new List<DamageEvent>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
       // if (File.Exists("damage.csv"))
       // {
       //     StreamWriter writer = File.AppendText("damage.csv");
       //     writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
       //     writer.Close();
       // }
       // else
       // {
       //     StreamWriter writer = File.CreateText("sessions.csv");
       //     writer.WriteLine("session_id;username;session_start;session_end");
       //     writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.username + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));
       //     writer.Close();
       // }
    }


}
