using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserController : EnemyController {

    public List<Transform> guns = new List<Transform>();

    private bool engaging = false;
    private bool inRange = false;
    private bool firing = false;

    void Start()
    {
        fleet = GameObject.FindGameObjectsWithTag("Enemy");
        stateMachine = new StateMachine();
        yamato = GameObject.FindGameObjectWithTag("Yamato");
        stateMachine.ChangeState(new IdleCruiserState(gameObject));
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).tag == "Gun")
            {
                guns.Add(transform.GetChild(i));
            }
        }
    }

    void Update () {

        if (yamatoSpotted && !engaging && !firing)
        {
            engaging = true;
            BroadcastYamatoLocation();
            stateMachine.ChangeState(new EngagingCruiserState(gameObject));
        }
        else if (!yamatoSpotted)
        {
            LookForYamato();
        }
        else if (engaging && !inRange)
        {
            inRange = CheckRange();
        }
        else if (inRange)
        {
            firing = true;
            stateMachine.ChangeState(new FiringCruiserState(gameObject));
        }
    }

    public void FireWeapons()
    {

    }
}
