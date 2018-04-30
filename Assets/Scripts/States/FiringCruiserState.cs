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
        Debug.Log("In firing state");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        ship.GetComponent<CruiserController>().FireWeapons();
    }
}
