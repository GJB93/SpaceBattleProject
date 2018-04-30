using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarrierController : EnemyController
{
    public List<Transform> spawners = new List<Transform>();
    public GameObject fighterPrefab;
    public int numOfFighters = 5;

    private bool engaging = false;
    private List<Boid> leaders = new List<Boid>();

    void Start()
    {
        fleet = FindObjectsOfType<EnemyController>().ToArray();
        stateMachine = new StateMachine();
        yamato = GameObject.FindGameObjectWithTag("Yamato");
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).tag == "Spawner")
            {
                spawners.Add(transform.GetChild(i));
            }
        }
    }

    void Update()
    {
        if (yamatoSpotted && !engaging)
        {
            engaging = true;
            BroadcastYamatoLocation();
            stateMachine.ChangeState(new CarrierAttackState(gameObject));
        }
        else if (!yamatoSpotted)
        {
            yamatoSpotted = LookForTarget(yamato.transform.position);
        }
        if (stateMachine.state != null)
        {
            stateMachine.Update();
        }
    }

    public void SpawnFighter()
    {
        for (int i = 0; i <= numOfFighters; i += 1)
        {
            for (int s = 0; s < spawners.Count; s += 1)
            {
                GameObject fighter = Instantiate(fighterPrefab, spawners.ElementAt(s));
                if(i == 0)
                {
                    leaders.Add(fighter.GetComponent<Boid>());
                }
                else
                {
                    fighter.AddComponent<OffsetPursue>().leader = leaders.ElementAt(s);
                    fighter.GetComponent<OffsetPursue>().weight = 4;
                    fighter.GetComponent<Wander>().weight = 0;
                }
            }
        }
    }
}
