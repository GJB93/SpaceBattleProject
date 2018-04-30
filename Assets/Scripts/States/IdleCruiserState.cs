using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCruiserState : CruiserState {

    public IdleCruiserState(GameObject cruiser):base(cruiser)
    {
    }

    public override void Enter()
    {
        if (cruiser.GetComponent<Pursue>() != null)
        {
            cruiser.GetComponent<Pursue>().weight = 0;
            cruiser.GetComponent<Pursue>().target = null;
        }
    }

    public override void Exit()
    {
        if (cruiser.GetComponent<Wander>() != null)
        {
            cruiser.GetComponent<Wander>().weight = 0;
        }
    }

    public override void Update()
    {
    }
}
