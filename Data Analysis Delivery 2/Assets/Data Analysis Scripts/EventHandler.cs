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

}
