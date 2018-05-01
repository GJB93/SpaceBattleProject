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
		if (GameObject.FindGameObjectWithTag("YamatoPath").GetComponent<Path>().next == 1 && !currentCamera.gameObject.tag.Equals("EnemyFleetCamera") 
            && GameObject.FindGameObjectWithTag("EnemyFleetCamera") != null)
        {
            currentCamera = GameObject.FindGameObjectWithTag("EnemyFleetCamera").GetComponent<Camera>();
            currentCamera.transform.LookAt(yamato.transform.position);
            stateMachine.ChangeState(new FollowCameraState(currentCamera));
        }

        if (!currentCamera.gameObject.tag.Equals("BirdsnestCamera") && yamato.GetComponent<YamatoController>().inRange && !yamato.GetComponent<YamatoController>().enemiesWiped)
        {
            currentCamera = GameObject.FindGameObjectWithTag("BirdsnestCamera").GetComponent<Camera>();
            if(yamato.GetComponent<YamatoController>().closestTarget != null)
            {
                currentCamera.GetComponent<FollowCamera>().target = yamato.GetComponent<YamatoController>().closestTarget.transform;
            }
            stateMachine.ChangeState(new FollowCameraState(currentCamera));
        }
        
        if (!currentCamera.gameObject.tag.Equals("CockpitCamera") && yamato.GetComponent<YamatoController>().enemiesWiped)
        {
            GameObject leadFighter = GameObject.FindGameObjectWithTag("LeadFighter");
            int count = leadFighter.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (leadFighter.transform.GetChild(i).tag == "CockpitCamera")
                {
                    currentCamera = leadFighter.transform.GetChild(i).GetComponent<Camera>();
                }
            }
            stateMachine.ChangeState(new FollowCameraState(currentCamera));
        }
    }
}
