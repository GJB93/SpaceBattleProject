using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagingCruiserState : CruiserState
{

    public EngagingCruiserState(GameObject cruiser) : base(cruiser)
    {
    }

    public override void Enter()
    {
        if (cruiser.GetComponent<Pursue>() != null && cruiser.GetComponent<Wander>() != null)
        {
            cruiser.GetComponent<Pursue>().weight = 5;
            cruiser.GetComponent<Pursue>().target = GameObject.FindGameObjectWithTag("Yamato").GetComponent<Boid>();
            cruiser.GetComponent<Wander>().weight = 0;
        }
    }

    public override void Exit()
    {
        if (cruiser.GetComponent<Wander>() != null)
        {
            cruiser.GetComponent<Wander>().weight = 1;
        }
    }

    public override void Update()
    {
    }
}
