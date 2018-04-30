using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float shipAttackRange = 100.0f;
    public StateMachine stateMachine;
    public float fieldOfView = 90.0f;
    public LayerMask environmentMask = 0;

    protected bool CheckRange(Vector3 target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget < shipAttackRange)
        {
            return true;
        }
        return false;
    }

    protected bool LookForTarget(Vector3 target)
    {
        Vector3 toTarget = (transform.position - target).normalized;
        if (Vector3.Angle(transform.forward, toTarget) < fieldOfView / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target);
            if (Physics.Raycast(transform.position, toTarget, distanceToTarget, environmentMask))
            {
                return true;
            }
        }
        return false;
    }
}
