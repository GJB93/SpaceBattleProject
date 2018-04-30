using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ShipController {
    
    public EnemyController[] fleet;

    protected GameObject yamato;
    protected bool yamatoSpotted = false;

    protected void SetYamatoSpotted(bool value)
    {
        yamatoSpotted = value;
    }

    protected void BroadcastYamatoLocation()
    {
        foreach(EnemyController ship in fleet)
        {
            ship.SetYamatoSpotted(true);
        }
    }
}
