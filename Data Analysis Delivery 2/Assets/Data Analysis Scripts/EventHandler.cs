using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewDamageEvent()
    {
        DamageEvent damageEvent = null;
        //TODO
        if (player)
            damageEvent.position = player.transform.position;

        
    }

}
