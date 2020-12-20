using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventHandler : MonoBehaviour
{
    public GameObject player;
    private Writer writer;

    private int EnemyKillCount = 0;
    private int HealthTimesCount = 0;
    private int DestroyCrateCount = 0;
    private int DoorOpenCount = 0;
    private int[,] positions;

    //Time in seconds
    float timer_since_start = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        writer = gameObject.GetComponent<Writer>();
        writer.SessionStart();
    }

    // Update is called once per frame
    void Update()
    {
        //Increase time each code iteration
        timer_since_start += Time.deltaTime;
    }

    public void EventSessionFinished()
    {
        writer.SessionFinished(timer_since_start); 
    }

    //Damage

    public void NewDamageEvent()
    {
        DamageEvent damageEvent = new DamageEvent();

        if (player)
        {
            damageEvent.position = player.transform.position;
            damageEvent.seconds_since_start = timer_since_start;
        }

        writer.AddDamageEvent(damageEvent);

    }

    //Death

    public void NewDeathEvent()
    {
        DeathEvent deathEvent   = new DeathEvent();

        if (player)
        {
            deathEvent.position = player.transform.position;
            deathEvent.eulerAngles = player.transform.eulerAngles;
        }

        writer.AddDeathEvent(deathEvent);

    }

    //Kill
    
    public void NewKillEnemiesEvent()
    {
        KillEvent attackEvent = new KillEvent();

        if (player)
        {
            EnemyKillCount++;
            attackEvent.seconds_since_start = timer_since_start;
            attackEvent.enemies_killed = EnemyKillCount;
        }

        writer.AddKillEvent(attackEvent);

    }

    //Healing
    
    public void NewHealingEvent()    {
        HealingEvent healingEvent = new HealingEvent();

        if (player)
        {
            HealthTimesCount++;
            healingEvent.seconds_since_start = timer_since_start;
            healingEvent.health_num = HealthTimesCount;
      
        }

        writer.AddHealingEvent(healingEvent);

    }
    

    // Destroy Crate 

    
    public void NewDestroyCrateEvent()
    {
        DestroyCrateEvent destroycrateEvent = new DestroyCrateEvent();

        if (player)
        {
            DestroyCrateCount++;
            destroycrateEvent.seconds_since_start = timer_since_start;
            destroycrateEvent.position = player.transform.position;
            destroycrateEvent.crates_destroyed = DestroyCrateCount;
        }

        writer.AddDestroyCrateEvent(destroycrateEvent);

    }
    

    //Door Event
     public void NewDoorEvent()
     {
         DoorEvent doorEvent = new DoorEvent();

         if (player)
         {
            DoorOpenCount++;
            doorEvent.seconds_since_start = timer_since_start;
            doorEvent.door_num =DoorOpenCount;
         }

         writer.AddDoorEvent(doorEvent);

     }
    public void NewPositionsEvent()
    {
        PositionEvent positionEvent = new PositionEvent();

        if (player)
        {
            positionEvent.seconds_since_start = timer_since_start;

            positionEvent.position.x = player.transform.position.x;
            positionEvent.position.y = player.transform.position.y;
            positionEvent.position.z = player.transform.position.z;

        }

        writer.AddPositionEvent(positionEvent);

    }


}
