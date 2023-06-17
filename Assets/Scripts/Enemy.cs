using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public struct EnemyStats
    {
        public float health;
        public float min_attack_distance;
        public float range_attack_distance;
        public bool has_range_attack;
        public float damage;
        public float speed;
    }
    public GameObject player;
    public Transform shootingPoint;
    private GameObject bulletPrefab;
    public Animator animator;
    public float health = 100;
    public float shootTimeout = 8f;
    private AudioSource bloodSound;

    private bool canShoot = true;
    private Transform target;
    private Player playerScript;
    private EnemyStats enemy;
    private bool canTakeDamage = true;
    private float dist;
    private float height;
    private float currSpeed;
    private float y_init;
    private CameraShake cameraShake;
    private EnemySpawner enemySpawner;
    private StatsManager statsManager;
    private LevelChanger levelChanger;

    void Start()
    {
        levelChanger = GameObject.Find("Level").GetComponent<LevelChanger>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        bloodSound = GameObject.Find("BloodSFX").GetComponent<AudioSource>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        y_init = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        enemy = new EnemyStats();
        switch (gameObject.tag)
        {
            case "archer":
                enemy.health = 2 + StatsManager.runDistance/30f;
                enemy.min_attack_distance = 3f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 6f;
                enemy.damage = 3 + StatsManager.runDistance/30f;
                break;
            case "widow":
                enemy.health = Mathf.Infinity;
                enemy.min_attack_distance = 3f;
                enemy.speed = StatsManager.playerMSpeed*1.3f;
                enemy.has_range_attack = true;
                enemy.range_attack_distance = 20f;
                enemy.damage = 10 + StatsManager.runDistance/30f;
                break;
            case "mush":
                enemy.health = 2 + StatsManager.runDistance/30f;
                enemy.min_attack_distance = 3f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 0f;
                enemy.damage = 3 + StatsManager.runDistance/30f;
                break;
            case "bomb":
                enemy.health = 1 + StatsManager.runDistance/30f;
                enemy.min_attack_distance = 3f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 4f;
                enemy.damage = 4 + StatsManager.runDistance/30f;
                break;
        }
        currSpeed = enemy.speed;
    }

    void Update()
    {
        if (gameObject.tag == "widow")
        {
            currSpeed = StatsManager.playerMSpeed*1.1f;
        }

        if (StatsManager.playerShootLevel == 0)
        {
            bulletPrefab = StatsManager.orangeStaticPrefabs[StatsManager.playerShootStyle];
        }
        else if (StatsManager.playerShootLevel == 1)
        {
            bulletPrefab = StatsManager.blueStaticPrefabs[StatsManager.playerShootStyle];
        }
        else if (StatsManager.playerShootLevel == 2)
        {
            bulletPrefab = StatsManager.greenStaticPrefabs[StatsManager.playerShootStyle];
        }
        else if (StatsManager.playerShootLevel == 3)
        {
            bulletPrefab = StatsManager.purpleStaticPrefabs[StatsManager.playerShootStyle];
        }

        // dist between player and enemy
        dist = Vector3.Distance(target.position, transform.position);
        height = transform.position.y;

        // always follow player with the enemy.speed value but always on y_init
        transform.position = new Vector3(transform.position.x + enemy.speed * Time.deltaTime, y_init, transform.position.z);
        animator.SetBool("idle", Mathf.Abs(target.position.x - transform.position.x) < 0.1f || enemy.speed == 0);
        animator.SetBool("run", Mathf.Abs(target.position.x - transform.position.x) >= 0.1f && enemy.speed > 0);

        // flip enemy sprite
        if (target.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // if this enemy is to the left of the player, destroy this enemy
        if (transform.position.x < target.position.x - 20 && gameObject.tag != "widow")
        {
            Destroy(gameObject);
            EnemySpawner.mobCnt--;
        }

        // debug log the transform.position.x of the player and the enemy
        //if (gameObject.tag == "widow")
            //Debug.Log("Player: " + target.position.x + " Enemy: " + transform.position.x + " Dist: " + (transform.position.x >= target.position.x));

        // if player is to the left of the widow, destroy the player
        if (transform.position.x >= target.position.x && gameObject.tag == "widow")
        {
            playerScript.TakeDamage(10000);
            StartCoroutine(ResetDamageCooldown());
        }

        // if dist between player and enemy is less than min_attack_distance, attack
        if (dist <= enemy.min_attack_distance && canTakeDamage && enemy.health > 0 && Mathf.Abs(target.position.y - transform.position.y) < 1f)
        {
            currSpeed = enemy.speed;
            enemy.speed = 0;
            // if (gameObject.tag == "bomb")
            // {
            //     animator.SetTrigger("attack");
            //     playerScript.TakeDamage(enemy.damage);
            //     StartCoroutine(ResetDamageCooldown());
            //     Invoke("DestroyObject", 1.0f);
            //     EnemySpawner.mobCnt--;
            // }
            // else
            // {
                animator.SetTrigger("attack");
                playerScript.TakeDamage(enemy.damage);
                StartCoroutine(ResetDamageCooldown());
            // }
            Invoke("ResetSpeed", 1f);
        }
        if (dist > enemy.min_attack_distance && dist <= enemy.range_attack_distance && enemy.has_range_attack && canShoot && Mathf.Abs(target.position.y - transform.position.y) < 1f)
        {
            animator.SetTrigger("shoot");
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            StartCoroutine(ShootCooldown());
        }
        if (dist <= 0.4f && gameObject.tag == "widow")
        {
            animator.SetTrigger("attack");
            playerScript.TakeDamage(10000);
            StartCoroutine(ResetDamageCooldown());
        }
    }

    public void TakeDamage (float damage)
    {
        enemy.speed = 0;
        enemy.health = enemy.health - damage;
        bloodSound.Play();
        Debug.Log("Enemy hit");
        StartCoroutine(cameraShake.Shake(.15f, .15f));
        animator.SetTrigger("hit");
        // if (gameObject.tag == "bomb")
        // {
        //     deadEnemies++;
        //     animator.SetTrigger("attack");
        //     if (dist <= enemy.range_attack_distance)
        //         playerScript.TakeDamage(enemy.damage);
        //     Invoke("DestroyObject", 1.0f);
        //     EnemySpawner.mobCnt--;
        // }
        if (enemy.health <= 0 && gameObject.tag != "widow")
        {
            statsManager.AddCoins(20);
            levelChanger.ExperienceIncrease(30);
            animator.SetTrigger("die");
            Invoke("DestroyObject", 1.0f);
            EnemySpawner.mobCnt--;
        }
        else
        {
            Invoke("ResetSpeed", 0.5f);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void ResetSpeed()
    {
        enemy.speed = currSpeed;
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootTimeout);
        canShoot = true;
    }

    private IEnumerator ResetDamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.2f);
        canTakeDamage = true;
    }
}
