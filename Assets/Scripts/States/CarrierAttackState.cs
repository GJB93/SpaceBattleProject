using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierAttackState : ShipState
{
    public CarrierAttackState(GameObject ship) : base(ship)
    {
    }

    public override void Enter()
    {
        ship.GetComponent<CarrierController>().SpawnFighter();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
