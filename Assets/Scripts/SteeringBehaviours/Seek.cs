using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour {

    public GameObject targetObject = null;
    public Vector3 target;

	void Update () {
		if (targetObject != null)
        {
            target = targetObject.transform.position;
        }
    }

    public override Vector3 Calculate()
    {
        return SeekForce(target);
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= boid.maxSpeed;
        return desired - boid.velocity;
    }
}
