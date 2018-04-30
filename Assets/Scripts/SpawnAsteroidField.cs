using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnAsteroidField : MonoBehaviour {

    public List<GameObject> asteroidPrefabs = new List<GameObject>();
    public int fieldWidth = 0;
    public int fieldLength = 0;
    public int fieldHeight = 0;
    public float heightScale = 1.0f;
    public float offsetScale = 3.0f;

    private int startZ = 0;
    private int startX = 0;

	void Awake () {
        startX = Mathf.RoundToInt(transform.position.x);
        startZ = Mathf.RoundToInt(transform.position.z);
        for (int z = startZ; z < fieldWidth + startZ; z += 1)
        {
            for (int x = startX; x < fieldLength + startX; x += 1)
            {
                int randomAsteroid = Random.Range(0, asteroidPrefabs.Count);
                float y = heightScale * Random.Range(-fieldHeight, fieldHeight);
                GameObject asteroid = Instantiate(asteroidPrefabs.ElementAt(randomAsteroid));
                asteroid.transform.position = new Vector3(x + offsetScale, y, z + offsetScale);
                asteroid.transform.parent = transform;
            }
        }
    }
	
	void Update () {
		
	}
}
