using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : ShipController {

    public GameObject laserPrefab;
    public GameObject[] enemies;
    public GameObject closestTarget;
    public bool enemyFighter = false;

    private bool engaged = false;
    private bool inRange = false;
    private bool waitToFire = false;


    void Start () {
        stateMachine = new StateMachine();
        if (!enemyFighter)
        {
            enemies = GameObject.FindGameObjectsWithTag("EnemyFighter");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("FriendlyFighter");
        }
        
    }
	
	void Update () {
        float minDistance = 10000000000.0f;
        
        if (!enemyFighter)
        {
            enemies = GameObject.FindGameObjectsWithTag("EnemyFighter");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("FriendlyFighter");
        }
        
        foreach (GameObject enemy in enemies)
        {
            if (CheckRange(enemy.transform.position) < minDistance)
            {
                minDistance = CheckRange(enemy.transform.position);
                closestTarget = enemy;
            }
        }
        if (closestTarget != null)
        {
            if (gameObject.tag.Equals("LeadFighter") || gameObject.tag.Equals("Lead Enemy Fighter"))
            {
                GetComponent<Seek>().target = closestTarget.transform.position;
            }
            if ((CheckRange(closestTarget.transform.position) < shipAttackRange))
            {
                FireWeapons();
            }
        }
        
    }

    public void FireWeapons()
    {
        if (!waitToFire && closestTarget != null)
        {
            GameObject projectile = Instantiate(laserPrefab, transform.position, transform.rotation);
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.velocity = (closestTarget.transform.position - transform.position).normalized * 100;
            waitToFire = true;
            StartCoroutine(WaitToFire());
        }

    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(3.0f);
        waitToFire = false;
    }
}
