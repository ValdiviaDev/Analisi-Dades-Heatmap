using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventHandler : MonoBehaviour
{
    public GameObject player;
    private Writer writer;
    


    // Start is called before the first frame update
    void Start()
    {
        writer = gameObject.GetComponent<Writer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewDamageEvent()
    {
        DamageEvent damageEvent = new DamageEvent();

        if (player)
            damageEvent.position = player.transform.position;

        writer.AddDamageEvent(damageEvent);

    }

}
