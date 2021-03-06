﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
{
    public static Writer Instance;
    private uint sessionID = 0u;

    private List<PositionEvent> posEvent = new List<PositionEvent>();

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

    public void AddPositionEvent(PositionEvent pos)
    {
        if(posEvent != null)
        {
            Instance.posEvent.Add(pos);
        }
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

    public static void Print()
    {
        Instance._Print();
    }

    private void _Print()
    {
        string parent_path = "Assets/CSV/";

        //Session data
        if (File.Exists(parent_path + "session_data.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "session_data.csv");

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.total_time_spent + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "session_data.csv");

            writer.WriteLine("session_id;total_time_spent;time_session_start;time_session_end");

            writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + Instance.total_time_spent + ";" + Instance.sessionStartTime.ToString("dd/MM/yyyy hh:mm:ss") + ";" + Instance.sessionEndTime.ToString("dd/MM/yyyy hh:mm:ss"));

            writer.Close();
        }

        //Damage
        if (File.Exists(parent_path + "damage.csv"))
       {
           StreamWriter writer = File.AppendText(parent_path + "damage.csv");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.eventID.ToString("0000000000") + ";" + damage.seconds_since_start + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
            }
      
           writer.Close();
       }
       else
       {
           StreamWriter writer = File.CreateText(parent_path + "damage.csv");
      
           writer.WriteLine("session_id;event_id;seconds_since_start;position_x;position_y;position_z");
      
           foreach (DamageEvent damage in Instance.damageList)
           {
               writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + damage.eventID.ToString("0000000000") + ";" + damage.seconds_since_start + ";" + damage.position.x + ";" + damage.position.y + ";" + damage.position.z);
           }
           writer.Close();
       }

        //Death


        if (File.Exists(parent_path + "death.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "death.csv");

            foreach (DeathEvent death in Instance.deathList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + death.eventID.ToString("0000000000") + ";" + death.seconds_since_start + ";" + death.position.x + ";" + death.position.y + ";" + death.position.z + ";" + death.eulerAngles.x + ";" + death.eulerAngles.y + ";" + death.eulerAngles.z);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "death.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;position_x;position_y;position_z;euler_x;euler_y;euler_z");

            foreach (DeathEvent death in Instance.deathList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + death.eventID.ToString("0000000000") + ";" + death.seconds_since_start + ";" + death.position.x + ";" + death.position.y + ";" + death.position.z + death.eulerAngles.x + ";" + death.eulerAngles.y + ";" + death.eulerAngles.z);
            }
            writer.Close();
        }

        //Kill


        if (File.Exists(parent_path + "enemieskill.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "enemieskill.csv");

            foreach (KillEvent kill in Instance.killList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + kill.eventID.ToString("0000000000") + ";" + kill.seconds_since_start + ";" + kill.enemies_killed);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "enemieskill.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;enemies_killed;");

            foreach (KillEvent kill in Instance.killList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + kill.eventID.ToString("0000000000") + ";" + kill.seconds_since_start + ";" + kill.enemies_killed);
            }
            writer.Close();
        }


        //Healing

        if (File.Exists(parent_path + "healing.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "healing.csv");

            foreach (HealingEvent healing in Instance.healingList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + healing.eventID.ToString("0000000000") + ";" + healing.seconds_since_start + ";" + healing.health_num);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "healing.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;health_num;");

            foreach (HealingEvent healing in Instance.healingList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + healing.eventID.ToString("0000000000") + ";" + healing.seconds_since_start + ";" + healing.health_num);
            }
            writer.Close();
        }

        //DestroyCrate

        if (File.Exists(parent_path + "destroy_crate.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "destroy_crate.csv");

            foreach (DestroyCrateEvent destroy in Instance.destroyList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + destroy.eventID.ToString("0000000000") + ";" + destroy.position.x + ";" + destroy.position.y + ";" + destroy.position.z + ";" + destroy.seconds_since_start + ";" + destroy.crates_destroyed);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "destroy_crate.csv");

            writer.WriteLine("session_id;event_id;position_x;position_y;position_z;seconds_since_start;crates_destroyed");

            foreach (DestroyCrateEvent destroy in Instance.destroyList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + destroy.eventID.ToString("0000000000") + ";" +  destroy.position.x + ";" + destroy.position.y + ";" + destroy.position.z + ";" + destroy.seconds_since_start + ";" + destroy.crates_destroyed);
            }
            writer.Close();
        }

        //Door Event

        if (File.Exists(parent_path + "door.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "door.csv");

            foreach (DoorEvent door in Instance.doorList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + door.eventID.ToString("0000000000") + ";" + door.door_num + ";" );
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "door.csv");

            writer.WriteLine("session_id;event_id;seconds_since_start;door_num");

            foreach (DoorEvent door in Instance.doorList)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + door.eventID.ToString("0000000000") + ";" + door.seconds_since_start + ";" + door.door_num + ";" );
            }
            writer.Close();
        }

        if (File.Exists(parent_path + "positions.csv"))
        {
            StreamWriter writer = File.AppendText(parent_path + "positions.csv");

            foreach (PositionEvent pos_aux in Instance.posEvent)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") + ";" + pos_aux.seconds_since_start + ";" + pos_aux.position.x + ";" + pos_aux.position.y + ";" + pos_aux.position.z);
            }

            writer.Close();
        }
        else
        {
            StreamWriter writer = File.CreateText(parent_path + "positions.csv");

            writer.WriteLine("session_id;seconds_since_start;position_x;position_y;position_z");

            foreach (PositionEvent pos_aux in Instance.posEvent)
            {
                writer.WriteLine(Instance.sessionID.ToString("0000000000") +  ";" + pos_aux.seconds_since_start + ";" + pos_aux.position.x + ";" + pos_aux.position.y + ";" + pos_aux.position.z);
                writer.WriteLine(Instance.sessionID.ToString("0000000000") +  ";" + pos_aux.seconds_since_start + ";" + pos_aux.position.x + ";" + pos_aux.position.y + ";" + pos_aux.position.z);
            }
            writer.Close();
        }

    }


}
