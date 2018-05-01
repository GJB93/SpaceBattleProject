using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target = null;
    
	void Start () {
		
	}
	
	void Update () {
        if (target == null)
        {
            transform.LookAt(transform.parent.forward);
        }
        else
        {
            transform.LookAt(target.transform);
        }
	}
}
