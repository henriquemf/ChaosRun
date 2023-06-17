using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 12f;
    public GameObject[] enemyPrefabs;
    public bool canSpawn = true;
    public static int mobCnt;
    public List<Transform> spawnPoints = new List<Transform>();

    private Transform target;
    private string currentCharacter;
    private bool canSpown;
    Vector3 initialPosition;
    string activeSceneName;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (mobCnt < 3)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    GameObject prefab = enemyPrefabs[Random.Range(0, 3)];
                    Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
                    mobCnt++;
                    yield return new WaitForSeconds(spawnRate);
                }
            }
        }
    }

}