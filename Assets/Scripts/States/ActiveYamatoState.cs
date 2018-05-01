using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveYamatoState : ShipState
{

    public ActiveYamatoState(GameObject ship) : base(ship)
    {
    }

    public override void Enter()
    {
        ship.GetComponent<YamatoController>().SpawnFighter();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        ship.GetComponent<YamatoController>().FireWeapons();
    }
}
