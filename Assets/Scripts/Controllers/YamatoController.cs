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
    public bool enemiesWiped = false;

    private bool underAttack = true;
    private bool engaged = false;
    public bool inRange = false;
    private GameObject leader;
    private bool waitToFire = false;
    public GameObject closestTarget;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            Gizmos.color = Color.blue;
            foreach (GameObject enemy in enemies)
            {
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }
    }

    void Start () {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleYamatoState(gameObject));
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
	
	void Update () {
		if(!inRange)
        {
            foreach(GameObject enemy in enemies)
            {
                if(CheckRange(enemy.transform.position) < shipAttackRange)
                {
                    inRange = true;
                }
            }
        }
        else
        {
            float minDistance = 10000000000.0f;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if(CheckRange(enemy.transform.position) < minDistance)
                {
                    minDistance = CheckRange(enemy.transform.position);
                    closestTarget = enemy;
                }
            }
            if(!engaged && inRange && enemies.Length > 0)
            {
                engaged = true;
                stateMachine.ChangeState(new ActiveYamatoState(gameObject));
            }
            if(enemies.Length == 0)
            {
                stateMachine.ChangeState(new IdleYamatoState(gameObject));
                enemiesWiped = true;
            }
        }
        stateMachine.Update();
	}

    public void SpawnFighter()
    {
        for (int i = 0; i <= numOfFighters; i += 1)
        {
            for (int s = 0; s < spawners.Count; s += 1)
            {
                GameObject fighter = Instantiate(fighterPrefab, spawners.ElementAt(s));
                if (i == 0 && s == 0)
                {
                    leader = fighter;
                    leader.AddComponent<Seek>().weight = 3;
                    leader.tag = "LeadFighter";
                }
                else
                {
                    fighter.AddComponent<OffsetPursue>().leader = leader.GetComponent<Boid>();
                    fighter.GetComponent<OffsetPursue>().weight = 1;
                }
                spawners.ElementAt(s).transform.DetachChildren();
            }
            
        }
    }

    public void FireWeapons()
    {
        if (!waitToFire && closestTarget != null)
        {
            foreach (Transform gun in guns)
            {
                GameObject projectile = Instantiate(laserPrefab, gun.position, gun.rotation);
                Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
                rigidbody.velocity = (closestTarget.transform.position - transform.position).normalized * 100;
            }
            waitToFire = true;
            StartCoroutine(WaitToFire());
        }

    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(0.5f);
        waitToFire = false;
    }
}
