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
    public static List<GameObject> activeEnemyPrefabs = new List<GameObject>();

    private Transform target;
    private string currentCharacter;
    private float lastSpawnTime = 0f;
    private AudioSource bloodSound;
    Vector3 initialPosition;
    string activeSceneName;

    // Start is called before the first frame update
    void Start()
    {
        bloodSound = GameObject.Find("BloodSFX").GetComponent<AudioSource>();
        activeSceneName = SceneManager.GetActiveScene().name;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (spawnPoints.Count > 0)
            StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        List<int> mobPerSpawnPoint = new List<int>(spawnPoints.Count);
        int index = 0;

        for (int i = 0; i < spawnPoints.Count; i++)
            mobPerSpawnPoint.Add(1);

        while (canSpawn)
        {
            yield return wait;
            if (activeEnemyPrefabs.Count == 0 && lastSpawnTime + spawnRate < Time.time)
            {
                lastSpawnTime = Time.time;
                for (int i = 0; i < mobPerSpawnPoint[index]; i++)
                {
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyToSpawn = enemyPrefabs[rand];
                    string enemyName;
                    
                    if (enemyToSpawn.CompareTag("archer"))
                    {
                        enemyName = "archer" + activeEnemyPrefabs.Count;
                    }
                    else if (enemyToSpawn.CompareTag("mush"))
                    {
                        enemyName = "mush" + activeEnemyPrefabs.Count;
                    }
                    else
                    {
                        enemyName = "bomb" + activeEnemyPrefabs.Count;
                    }
                    
                    activeEnemyPrefabs.Add(Instantiate(enemyToSpawn, spawnPoints[index].position, Quaternion.identity));
                    activeEnemyPrefabs[activeEnemyPrefabs.Count - 1].name = enemyName;
                }
            }
            index++;
        }
        yield return null;
    }
}