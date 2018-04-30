using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGuns : MonoBehaviour {

    public List<Transform> guns = new List<Transform>();
    public GameObject primaryTarget = null;

	// Use this for initialization
	void Start () {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if(transform.GetChild(i).tag == "Gun")
            {
                guns.Add(transform.GetChild(i));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		foreach(Transform gun in guns)
        {
            if (primaryTarget != null)
            {
                if (gun.root.tag.Equals("Yamato"))
                {
                    // Look in opposite direction to forward
                    gun.LookAt(2 * transform.position - primaryTarget.transform.position);
                }
                else
                {
                    gun.LookAt(primaryTarget.transform.position);
                }
            }
            else
            {
                if (gun.root.tag.Equals("Yamato"))
                {
                    // Look in opposite direction to forward
                    gun.LookAt(2 * transform.position - transform.root.forward);
                }
                else
                {
                    gun.LookAt(transform.root.forward);
                }
            }
        }
	}
}
