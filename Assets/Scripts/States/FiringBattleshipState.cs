using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringBattleshipState : ShipState
{

    public FiringBattleshipState(GameObject ship) : base(ship)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        ship.GetComponent<BattleshipController>().FireWeapons();
    }
}
