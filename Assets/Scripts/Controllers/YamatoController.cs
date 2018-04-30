using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YamatoController : ShipController {

    public GameObject[] enemies;
    public Transform bodyTransform;
    public GameObject laserPrefab;
    public List<Transform> spawners = new List<Transform>();
    public List<Transform> guns = new List<Transform>();
    public GameObject fighterPrefab;
    public int numOfFighters = 5;

    private bool underAttack = false;
    private List<Boid> leaders = new List<Boid>();

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).tag == "Spawner")
            {
                spawners.Add(transform.GetChild(i));
            }
        }
        bodyTransform = transform.GetChild(0).GetChild(0);
        count = bodyTransform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (bodyTransform.GetChild(i).tag == "Gun")
            {
                guns.Add(bodyTransform.GetChild(i));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(!underAttack)
        {
            foreach(GameObject enemy in enemies)
            {
                underAttack = LookForTarget(enemy.transform.position);
            }
        }
        else
        {

        }
	}

    public void SpawnFighter()
    {
        for (int i = 0; i <= numOfFighters; i += 1)
        {
            for (int s = 0; s < spawners.Count; s += 1)
            {
                GameObject fighter = Instantiate(fighterPrefab, spawners.ElementAt(s));
                if (i == 0)
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
