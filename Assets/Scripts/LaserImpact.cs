using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserImpact : MonoBehaviour {

    public GameObject explosionPrefab;
    public bool enemyLaser = false;

    private GameObject explosion;

    private void Awake()
    {
        StartCoroutine(BulletLifetime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enemyLaser && !collision.gameObject.tag.Equals("Yamato") && !collision.gameObject.tag.Equals("World") && !collision.gameObject.tag.Equals("FriendlyFighter") && !collision.gameObject.tag.Equals("LeadFighter"))
        {
            explosion = Instantiate(explosionPrefab, collision.contacts[0].point, Quaternion.identity);
            StartCoroutine(EndEffect());
            Destroy(collision.gameObject);
        }
        if (enemyLaser && !collision.gameObject.tag.Equals("Enemy"))
        {
            explosion = Instantiate(explosionPrefab, collision.contacts[0].point, Quaternion.identity);
            StartCoroutine(EndEffect());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enemyLaser && !other.gameObject.tag.Equals("Yamato") && !other.gameObject.tag.Equals("World") && !other.gameObject.tag.Equals("FriendlyFighter") && !other.gameObject.tag.Equals("LeadFighter"))
        {
            explosion = Instantiate(explosionPrefab, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);
            StartCoroutine(EndEffect());
            Destroy(other.gameObject);
        }
    }

    IEnumerator EndEffect()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(explosion);
        Destroy(gameObject);
    }

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
