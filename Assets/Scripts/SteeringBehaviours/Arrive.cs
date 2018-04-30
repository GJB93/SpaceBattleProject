using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteeringBehaviour {

    public GameObject targetObject = null;
    public Vector3 target;
    public float slowingDistance = 15.0f;

    [Range(0.0f, 1.0f)]
    public float deceleration = 0.9f;

    void Update()
    {
        if (targetObject != null)
        {
            target = targetObject.transform.position;
        }
    }

    public override Vector3 Calculate()
    {
        return ArriveForce(target, slowingDistance, deceleration);
    }

    public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f, float deceleration = 1.0f)
    {
        Vector3 toTarget = target - transform.position;

        float distance = toTarget.magnitude;
        if (distance == 0)
        {
            return Vector3.zero;
        }
        float ramped = boid.maxSpeed * (distance / (slowingDistance * deceleration));

        float clamped = Mathf.Min(ramped, boid.maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);

        return desired - boid.velocity;
    }
}
