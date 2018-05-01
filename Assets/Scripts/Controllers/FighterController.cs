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
        }
        
    }

    public void FireWeapons()
    {
        Debug.Log("Firing Weapon");
        if (!waitToFire && closestTarget != null)
        {
            Debug.Log("Projectile");
            GameObject projectile = Instantiate(laserPrefab, transform.position, transform.rotation);
            Debug.Log("rigidbody");
            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            Debug.Log("velocity");
            rigidbody.velocity = (closestTarget.transform.position - transform.position).normalized * 100;
            Debug.Log("waittofire");
            waitToFire = true;
            Debug.Log("coroutine");
            StartCoroutine(WaitToFire());
            Debug.Log("aftercoroutine");
        }

    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(3.0f);
        waitToFire = false;
    }
}
