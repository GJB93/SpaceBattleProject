using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLifetime : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DestroyExplosionObject());
    }

    IEnumerator DestroyExplosionObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
