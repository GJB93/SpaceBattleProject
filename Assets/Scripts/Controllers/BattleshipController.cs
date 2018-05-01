using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleshipController : EnemyController
{

    public Transform bodyTransform;
    public GameObject laserPrefab;
    public List<Transform> guns = new List<Transform>();

    private bool engaging = false;
    private bool waitToFire = false;

    void Start()
    {
        fleet = FindObjectsOfType<EnemyController>().ToArray();
        stateMachine = new StateMachine();
        yamato = GameObject.FindGameObjectWithTag("Yamato");
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

    void Update()
    {
        if (yamatoSpotted && !engaging)
        {
            engaging = true;
            BroadcastYamatoLocation();
            stateMachine.ChangeState(new FiringBattleshipState(gameObject));
        }
        else if (!yamatoSpotted)
        {
            yamatoSpotted = CheckRange(yamato.transform.position) < shipAttackRange;
        }
        if (stateMachine.state != null)
        {
            stateMachine.Update();
        }
    }

    public void FireWeapons()
    {
        if (!waitToFire)
        {
            foreach (Transform gun in guns)
            {
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
