using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagingCruiserState : ShipState
{

    public EngagingCruiserState(GameObject cruiser) : base(cruiser)
    {
    }

    public override void Enter()
    {
        if (ship.GetComponent<Pursue>() != null && ship.GetComponent<Wander>() != null)
        {
            ship.GetComponent<Pursue>().weight = 5;
            Debug.Log("Setting target");
            ship.GetComponent<Pursue>().target = GameObject.FindGameObjectWithTag("Yamato").GetComponent<Boid>();
            ship.GetComponent<Wander>().weight = 0;
        }
    }

    public override void Exit()
    {
        if (ship.GetComponent<Wander>() != null)
        {
            ship.GetComponent<Wander>().weight = 1;
        }
    }

    public override void Update()
    {
    }
}
