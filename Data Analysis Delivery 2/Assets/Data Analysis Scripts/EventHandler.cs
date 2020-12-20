using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventHandler : MonoBehaviour
{
    public GameObject player;
    private Writer writer;

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

    //Attack
    /*public void NewAttackEvent()
    {
        AttackEvent attackEvent = new AttackEvent();

        if (player)
        {
            attackEvent.attacks_num = 
            attackEvent.enemies_killed = ;
        }

        writer.AddAttackEvent(attackEvent);

    }*/

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

}
