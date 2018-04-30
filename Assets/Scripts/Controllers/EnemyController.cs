using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public StateMachine stateMachine;
    public float fieldOfView = 90.0f;
    public LayerMask environmentMask = 0;
    public GameObject[] fleet;

    protected GameObject yamato;
    protected bool yamatoSpotted = false;
    protected float shipAttackRange = 100.0f;

    protected bool LookForYamato()
    {
        Vector3 toTarget = (transform.position - yamato.transform.position).normalized;
        if (Vector3.Angle(transform.forward, toTarget) < fieldOfView / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, yamato.transform.position);
            if (!Physics.Raycast(transform.position, toTarget, distanceToTarget, environmentMask))
            {
                Debug.Log("Yamato seen by enemy");
                return true;
            }
        }
        return false;
    }

    protected void SetYamatoSpotted(bool value)
    {
        this.yamatoSpotted = value;
    }

    protected void BroadcastYamatoLocation()
    {
        foreach(GameObject ship in fleet)
        {
            ship.GetComponent<EnemyController>().SetYamatoSpotted(true);
        }
    }

    protected bool CheckRange()
    {
        float distanceToTarget = Vector3.Distance(transform.position, yamato.transform.position);
        if (distanceToTarget < shipAttackRange)
        {
            return true;
        }
        return false;
    }
}
