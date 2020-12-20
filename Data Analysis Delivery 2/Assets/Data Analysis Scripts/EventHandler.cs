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

    //Time in seconds
    float timer_sicne_start = 0.0f;

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
        timer_sicne_start += Time.deltaTime;
    }

    public void EventSessionFinished()
    {
        writer.SessionFinished(timer_sicne_start); 
    }

    //Damage

    public void NewDamageEvent()
    {
        DamageEvent damageEvent = new DamageEvent();

        if (player)
        {
            damageEvent.position = player.transform.position;
            damageEvent.seconds_since_start = timer_sicne_start;
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
            attackEvent.seconds_since_start = timer_sicne_start;
            attackEvent.enemies_killed = EnemyKillCount;
        }

        writer.AddKillEvent(attackEvent);

    }

    //Healing
    /*
    public void NewHealingEvent()    {
        HealingEvent healingEvent = new HealingEvent();

        if (player)
        {
            healingEvent.health_num =
            healingEvent.hearts = 
        }

        writer.AddDeathEvent(healingEvent);

    }
    */

    // Destroy Crate 

    /*
    public void NewDestroyCrateEvent()
    {
        DestroyCrateEvent destroycrateEvent = new DestroyCrateEvent();

        if (player)
        {
            destroycrateEvent.position = player.transform.position;
            destroycrateEvent.crates_destroyed = 
        }

        writer.AddDestroyCrateEvent(destroycrateEvent);

    }
    */

    //Door Event
    /* public void NewDoorEvent()
     {
         DoorEvent doorEvent = new DoorEvent();

         if (player)
         {
             doorEvent.door_num =;

         }

         writer.AddDoorEvent(doorEvent);

     }*/

}
