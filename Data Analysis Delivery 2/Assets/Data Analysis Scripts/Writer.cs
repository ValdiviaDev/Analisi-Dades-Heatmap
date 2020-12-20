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
    private List<DeathEvent> deathList = new List<DeathEvent>();
    private List<HealingEvent> healingList = new List<HealingEvent>();
    private List<DestroyCrateEvent> destroyList = new List<DestroyCrateEvent>();
    private List<DoorEvent> doorList = new List<DoorEvent>();
    private List<KillEvent> killList = new List<KillEvent>();




    // Time
    private DateTime sessionStartTime;
    private DateTime sessionEndTime;
    float total_time_spent = 0.0f;

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

    public void SessionFinished(float total_time_spent)
    {
        Instance.sessionEndTime = DateTime.Now;
        this.total_time_spent = total_time_spent;

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

    public void AddDeathEvent(DeathEvent death)
    {
        if (death != null)
        {
            death.eventID = GenerateUUID();
            Instance.deathList.Add(death);
        }
    }

    public void AddKillEvent(KillEvent kill)
    {
        if (kill != null)
        {
            kill.eventID = GenerateUUID();
            Instance.killList.Add(kill);
        }
    }

    public void AddHealingEvent(HealingEvent healing)
    {
        if (healing != null)
        {
            healing.eventID = GenerateUUID();
            Instance.healingList.Add(healing);
        }
    }

    public void AddDestroyCrateEvent(DestroyCrateEvent destroy)
    {
        if (destroy != null)
        {
            destroy.eventID = GenerateUUID();
            Instance.destroyList.Add(destroy);
        }
    }

    public void AddDoorEvent(DoorEvent door)
    {
        if (door != null)
        {
            door.eventID = GenerateUUID();
            Instance.doorList.Add(door);
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

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.total_time_spent + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("session_data.csv");

            writer.WriteLine("session_id;total_time_spent;time_session_start;time_session_end");

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.total_time_spent + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

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

        //Death


        if (File.Exists("death.csv"))
        {
            StreamWriter writer = File.AppendText("death.csv");

            foreach (DeathEvent death in Instance.deathList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + death.eventID.ToString("0000000000") + ";" + death.seconds_since_start + ";" + death.position.x + ";" + death.position.y + ";" + death.position.z + ";" + death.eulerAngles.x + ";" + death.eulerAngles.y + ";" + death.eulerAngles.z);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("death.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;position_x;position_y;position_z;euler_x;euler_y;euler_z");

            foreach (DeathEvent death in Instance.deathList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + death.eventID.ToString("0000000000") + ";" + death.seconds_since_start + ";" + death.position.x + ";" + death.position.y + ";" + death.position.z + death.eulerAngles.x + ";" + death.eulerAngles.y + ";" + death.eulerAngles.z);
            }
            writer.Close();
        }

        //Kill


        if (File.Exists("enemieskill.csv"))
        {
            StreamWriter writer = File.AppendText("enemieskill.csv");

            foreach (KillEvent kill in Instance.killList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + kill.eventID.ToString("0000000000") + ";" + kill.seconds_since_start + ";" + kill.enemies_killed);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("enemieskill.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;enemies_killed;");

            foreach (KillEvent kill in Instance.killList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + kill.eventID.ToString("0000000000") + ";" + kill.seconds_since_start + ";" + kill.enemies_killed);
            }
            writer.Close();
        }


        //Healing

        if (File.Exists("healing.csv"))
        {
            StreamWriter writer = File.AppendText("healing.csv");

            foreach (HealingEvent healing in Instance.healingList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + healing.eventID.ToString("0000000000") + ";" + healing.hearts + ";" + healing.health_num);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("healing.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;hearts;health_num;");

            foreach (HealingEvent healing in Instance.healingList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + healing.eventID.ToString("0000000000") + ";" + healing.hearts + ";" + healing.health_num);
            }
            writer.Close();
        }

        //DestroyCrate

        if (File.Exists("destroy_crate.csv"))
        {
            StreamWriter writer = File.AppendText("destroy_crate.csv");

            foreach (DestroyCrateEvent destroy in Instance.destroyList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + destroy.eventID.ToString("0000000000") + ";" + destroy.crates_destroyed + ";" + destroy.position.x + ";" + destroy.position.y + ";" + destroy.position.z);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("destroy_crate.csv");

            writer.WriteLine("session_id;event_id;crates_destroyed;position_x;position_y;position_z;");

            foreach (DestroyCrateEvent destroy in Instance.destroyList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + destroy.eventID.ToString("0000000000") + ";" + destroy.crates_destroyed + ";" + destroy.position.x + ";" + destroy.position.y + ";" + destroy.position.z);
            }
            writer.Close();
        }

        //Door Event

        if (File.Exists("door.csv"))
        {
            StreamWriter writer = File.AppendText("door.csv");

            foreach (DoorEvent door in Instance.doorList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + door.eventID.ToString("0000000000") + ";" + door.door_num + ";" );
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText("door.csv");

            writer.WriteLine("session_id;event_id;door_num");

            foreach (DoorEvent door in Instance.doorList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + door.eventID.ToString("0000000000") + ";" + door.door_num + ";" );
            }
            writer.Close();
        }
    }


}
