using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CruiserController : EnemyController {

    public Transform bodyTransform;
    public GameObject laserPrefab;
    public List<Transform> guns = new List<Transform>();

    private bool engaging = false;
    private bool inRange = false;
    private bool firing = false;
    private bool waitToFire = false;

    void Start()
    {
        fleet = FindObjectsOfType<EnemyController>().ToArray();
        stateMachine = new StateMachine();
        yamato = GameObject.FindGameObjectWithTag("Yamato");
        stateMachine.ChangeState(new IdleCruiserState(gameObject));
        bodyTransform = transform.GetChild(1);
        int count = bodyTransform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (bodyTransform.GetChild(i).tag == "Gun")
            {
                guns.Add(bodyTransform.GetChild(i));
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
            yamatoSpotted = LookForTarget(yamato.transform.position);
        }
        else if (engaging && !inRange)
        {
            inRange = CheckRange(yamato.transform.position);
        }
        else if (inRange)
        {
            firing = true;
            stateMachine.ChangeState(new FiringCruiserState(gameObject));
        }

        stateMachine.Update();
    }

    public void FireWeapons()
    {
        if (!waitToFire)
        {
            foreach (Transform gun in guns)
            {
                Debug.Log("Firing weapon");
                GameObject projectile = Instantiate(laserPrefab, gun.position, gun.rotation);
                Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
                rigidbody.velocity = (yamato.transform.position - transform.position).normalized * 100;
            }
            waitToFire = true;
            StartCoroutine(WaitToFire());
        }
        
    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(1.0f);
        waitToFire = false;
    }
}
