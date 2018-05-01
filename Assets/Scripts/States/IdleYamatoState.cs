using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleYamatoState : ShipState
{
    public IdleYamatoState(GameObject ship) : base(ship)
    {
    }

    public override void Enter()
    {
        ship.GetComponent<FollowPath>().weight = 2;
    }

    public override void Exit()
    {
        ship.GetComponent<FollowPath>().weight = 1;
    }

    public override void Update()
    {
    }
}
