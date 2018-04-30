using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public GameObject target = null;
    
	void Start () {
		
	}
	
	void Update () {
        if (target == null)
        {
            transform.LookAt(transform.parent);
        }
        else
        {
            transform.LookAt(target.transform);
        }
	}
}
