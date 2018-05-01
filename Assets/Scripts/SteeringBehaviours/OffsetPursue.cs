using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour {

    public Boid leader = null;
    private Vector3 offset = Vector3.zero;
    private Vector3 worldTarget = Vector3.zero;
        
	void Start () {
        if (leader != null)
        {
            offset = transform.position - leader.transform.position;
            offset = Quaternion.Inverse(leader.transform.rotation) * offset;
        }
        else
        {
            Debug.LogError("Leader is not set for offset pursue");
        }
	}

    public override Vector3 Calculate()
    {
        if(leader != null)
        {
            worldTarget = leader.transform.TransformPoint(offset);
            float dist = Vector3.Distance(worldTarget, transform.position);
            float time = dist / boid.maxSpeed;

            Vector3 targetPos = worldTarget + (leader.velocity * time);
            return boid.ArriveForce(targetPos, 10);
        }
        return Vector3.zero;
    }

}
