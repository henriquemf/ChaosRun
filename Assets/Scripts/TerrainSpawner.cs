using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject terrainPrefab;
    private Transform actualTransform;
    private bool hasSpawnedThisInterval = false;
    private float spawnDistance = 30f;
    private List<GameObject> terrains = new List<GameObject>();

    void Start()
    {
        actualTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        float distanceToSpawn = Player.position - actualTransform.position.x;
        float distanceToDestroy;

        if(distanceToSpawn >= -spawnDistance && distanceToSpawn <= 0f && !hasSpawnedThisInterval)
        {
            SpawnTerrain();
            hasSpawnedThisInterval = true;
        }
        else if(distanceToSpawn < -60f)
        {
            hasSpawnedThisInterval = false;
        }

        for (int i = terrains.Count - 1; i >= 0; i--)
        {
            distanceToDestroy = terrains[0].transform.position.x - Player.position;
            if (distanceToDestroy < -10f && distanceToDestroy > -20f)
            {
                GameObject toDestroy = terrains[0];
                terrains.RemoveAt(0);
                Destroy(toDestroy);
            }
        }
    }

    void SpawnTerrain()
    {
        GameObject terrain = Instantiate(terrainPrefab);
        terrain.transform.position = new Vector3(actualTransform.position.x + 22, actualTransform.position.y + 5, actualTransform.position.z);
        terrains.Add(terrain);
        actualTransform.Translate(new Vector3(45f, 0, 0));
    }
}
