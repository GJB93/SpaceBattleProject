using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringCruiserState : ShipState
{

    public FiringCruiserState(GameObject ship) : base(ship)
    {
    }

    public override void Enter()
    {
        if (ship.GetComponent<Pursue>() != null)
        {
            ship.GetComponent<Pursue>().weight = 2;
            ship.GetComponent<Pursue>().target = GameObject.FindGameObjectWithTag("Yamato").GetComponent<Boid>();
        }
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        ship.GetComponent<CruiserController>().FireWeapons();
        if (ship.GetComponent<OffsetPursue>() != null)
        {
            if (ship.GetComponent<OffsetPursue>().leader == null)
            {
                Object.Destroy(ship.GetComponent<OffsetPursue>());
                ship.AddComponent<Pursue>().target = GameObject.FindGameObjectWithTag("Yamato").GetComponent<Boid>();
            }
        }
    }
}
