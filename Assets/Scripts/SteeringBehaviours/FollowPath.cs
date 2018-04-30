using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehaviour
{

    public Path path;

    private Vector3 nextWaypoint;
    private Arrive arrive;
    private Seek seek;

    public void Start(){
    }

    public override Vector3 Calculate()
    {
        nextWaypoint = path.NextWaypoint();
        if (Vector3.Distance(transform.position, nextWaypoint) < 3)
        {
            path.AdvanceToNext();
        }

        if (!path.looped && path.IsLast())
        {
            return boid.ArriveForce(nextWaypoint, 20);
        }
        else
        {
            return boid.SeekForce(nextWaypoint);
        }
    }
}