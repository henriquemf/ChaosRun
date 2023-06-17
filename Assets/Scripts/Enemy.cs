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
    public AudioSource bloodSound;

    private bool canShoot = true;
    private Transform target;
    private Player playerScript;
    private EnemyStats enemy;
    private bool canTakeDamage = true;
    private float dist;
    private float currSpeed;
    private float y_init;
    private CameraShake cameraShake;

    void Start()
    {
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
                enemy.health = 10 + StatsManager.runDistance/10f;
                enemy.min_attack_distance = 6f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 6f;
                enemy.damage = 20 + StatsManager.runDistance/10f;
                break;
            case "widow":
                enemy.health = 6000000000000;
                enemy.min_attack_distance = 5f;
                enemy.speed = StatsManager.playerMSpeed*1.1f;
                enemy.has_range_attack = true;
                enemy.range_attack_distance = 20f;
                enemy.damage = 20 + StatsManager.runDistance/10f;
                break;
            case "mush":
                enemy.health = 10 + StatsManager.runDistance/10f;
                enemy.min_attack_distance = 7f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 0f;
                enemy.damage = 20 + StatsManager.runDistance/10f;
                break;
            case "bomb":
                enemy.health = 1 + StatsManager.runDistance/10f;
                enemy.min_attack_distance = 4f;
                enemy.speed = 0;
                enemy.has_range_attack = false;
                enemy.range_attack_distance = 4f;
                enemy.damage = 80 + StatsManager.runDistance/10f;
                break;
        }
    }

    void Update()
    {
        enemy.health += StatsManager.runDistance/10f;
        enemy.damage += StatsManager.runDistance/10f;

        if (gameObject.tag == "widow")
        {
            enemy.speed = StatsManager.playerMSpeed*1.1f;
        }
        
        currSpeed = enemy.speed;

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

        // always follow player with the enemy.speed value but always on y_init
        transform.position = new Vector3(transform.position.x + enemy.speed * Time.deltaTime, y_init, transform.position.z);
        animator.SetBool("idle", Mathf.Abs(target.position.x - transform.position.x) < 0.1f || enemy.speed == 0);
        animator.SetBool("run", Mathf.Abs(target.position.x - transform.position.x) >= 0.1f && enemy.speed > 0);

        // flip enemy sprite
        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // if dist between player and enemy is less than min_attack_distance, attack
        if (dist <= enemy.min_attack_distance && canTakeDamage && enemy.health > 0)
        {
            Debug.Log("Player hit " + (dist <= enemy.min_attack_distance));
            currSpeed = enemy.speed;
            enemy.speed = 0;
            if (gameObject.tag == "bomb")
            {
                animator.SetTrigger("attack");
                playerScript.TakeDamage(enemy.damage);
                StartCoroutine(ResetDamageCooldown());
                Invoke("DestroyObject", 1.0f);
            }
            else
            {
                animator.SetTrigger("attack");
                playerScript.TakeDamage(enemy.damage);
                StartCoroutine(ResetDamageCooldown());
            }
            Invoke("ResetSpeed", 1f);
        }
        if (dist > enemy.min_attack_distance && dist <= enemy.range_attack_distance && enemy.has_range_attack && canShoot)
        {
            animator.SetTrigger("shoot");
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            StartCoroutine(ShootCooldown());
        }
        if (dist <= 1f && gameObject.tag == "widow")
        {
            animator.SetTrigger("attack");
            playerScript.TakeDamage(10000);
            StartCoroutine(ResetDamageCooldown());
        }
        // else if (gameObject.tag == "widow" && dist <= enemy.range_attack_distance && dist > enemy.min_attack_distance && canShoot)
        // {
        //     animator.SetTrigger("shoot");
        //     Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        //     StartCoroutine(ShootCooldown());
        // }
    }

    // for bullets
    // private void OnTriggerEnter2D(Collider2D hitInfo)
    // {
    //     Player player = hitInfo.GetComponent<Player>();
    //     if (player != null)
    //     {
    //         animator.SetTrigger("attack");
    //         Debug.Log("Player hit");
    //         player.TakeDamage(20);
    //     }
    // }

    // on colide with target, target takes damage
    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player" && canTakeDamage)
    //     {
    //         if (player != null)
    //         {
    //             animator.SetTrigger("attack");
    //             playerScript.TakeDamage(enemy.damage);
    //             StartCoroutine(ResetDamageCooldown());
    //         }
    //     }
    // }

    // if keeps coliiding with target, target takes damage
    // public void OnCollisionStay2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player" && canTakeDamage)
    //     {
    //         if (player != null)
    //         {
    //             animator.SetTrigger("attack");
    //             playerScript.TakeDamage(enemy.damage);
    //             StartCoroutine(ResetDamageCooldown());
    //         }
    //     }
    // }

    public void TakeDamage (float damage)
    {
        Debug.Log("Enemy hit");
        enemy.speed = 0;
        enemy.health = enemy.health - damage;
        bloodSound.Play();
        StartCoroutine(cameraShake.Shake(.15f, .15f));
        animator.SetTrigger("hit");
        if (gameObject.tag == "bomb")
        {
            animator.SetTrigger("attack");
            if (dist <= enemy.range_attack_distance)
                playerScript.TakeDamage(enemy.damage);
            Invoke("DestroyObject", 1.0f);
        }
        else if (enemy.health <= 0)
        {
            animator.SetTrigger("die");
            Invoke("DestroyObject", 1.0f);
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
