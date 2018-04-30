using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOrchestrator : MonoBehaviour {
    
    public StateMachine stateMachine;
    public GameObject yamato;
    public GameObject[] enemyFleet;
    public Camera currentCamera;
    public float enemyFov = 103.0f;
    public LayerMask environmentMask = -1;

    private bool yamatoInFov = false;
    
	void Start () {
        currentCamera = GameObject.FindGameObjectWithTag("FrontYamatoCamera").GetComponent<Camera>();

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new FollowCameraState(currentCamera));

        yamato = GameObject.FindGameObjectWithTag("Yamato");
        enemyFleet = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Update () {
		if (GameObject.FindGameObjectWithTag("YamatoPath").GetComponent<Path>().next == 1 && !currentCamera.gameObject.tag.Equals("EnemyFleetCamera"))
        {
            currentCamera = GameObject.FindGameObjectWithTag("EnemyFleetCamera").GetComponent<Camera>();
            stateMachine.ChangeState(new FollowCameraState(currentCamera));
        }

        /*
        foreach(GameObject enemy in enemyFleet)
        {
            Vector3 toTarget = (enemy.transform.position - yamato.transform.position).normalized;
            if(Vector3.Angle(enemy.transform.forward, toTarget) < enemyFov / 2)
            {
                float distanceToTarget = Vector3.Distance(enemy.transform.position, yamato.transform.position);
                if (!Physics.Raycast(enemy.transform.position, toTarget, 1.0f, environmentMask))
                {
                    Debug.Log("Yamato seen by enemy");
                    //yamatoInFov = true;
                }
            }
        }
        */

        if (!currentCamera.gameObject.tag.Equals("BirdsEyeCamera") && yamatoInFov)
        {
            currentCamera = GameObject.FindGameObjectWithTag("BirdsEyeCamera").GetComponent<Camera>();
            stateMachine.ChangeState(new FollowCameraState(currentCamera));
        }
    }
}
