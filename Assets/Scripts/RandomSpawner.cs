using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> randomPrefabs;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private float spawnDistance = 30f;
    private Transform actualTransform;
    private float lastSpawnPosition;
    private bool hasSpawnedThisInterval = false;

    private void Start()
    {
        actualTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        float distanceToSpawn = Player.position - actualTransform.position.x;
        float distanceToDestroy;

        if (distanceToSpawn >= -spawnDistance && distanceToSpawn <= 0f && !hasSpawnedThisInterval)
        {
            SpawnRandom();
            hasSpawnedThisInterval = true;
        }

        else if (distanceToSpawn < -60f)
        {
            hasSpawnedThisInterval = false;
        }

        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            distanceToDestroy = spawnedObjects[0].transform.position.x - Player.position;
            if (distanceToDestroy < -50f && distanceToDestroy > -60f)
            {
                GameObject toDestroy = spawnedObjects[0];
                spawnedObjects.RemoveAt(0);
                Destroy(toDestroy);
            }
        }
    }

    void SpawnRandom()
    {
        GameObject randomPrefab = randomPrefabs[Random.Range(0, randomPrefabs.Count)];
        GameObject random = Instantiate(randomPrefab);
        random.transform.position = new Vector3(actualTransform.position.x + 22, actualTransform.position.y + 5, actualTransform.position.z);
        spawnedObjects.Add(random);
        actualTransform.Translate(new Vector3(45f, 0, 0));
    }
}
