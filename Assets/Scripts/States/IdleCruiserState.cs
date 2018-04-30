using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCruiserState : ShipState
{
    public IdleCruiserState(GameObject ship):base(ship)
    {
    }

    public override void Enter()
    {
        if (ship.GetComponent<Pursue>() != null)
        {
            ship.GetComponent<Pursue>().weight = 0;
            ship.GetComponent<Pursue>().target = null;
        }
    }

    public override void Exit()
    {
        if (ship.GetComponent<Wander>() != null)
        {
            ship.GetComponent<Wander>().weight = 0;
        }
    }

    public override void Update()
    {
    }
}
