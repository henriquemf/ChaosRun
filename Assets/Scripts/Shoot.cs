using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform shootingPoint;
    private GameObject bulletPrefab;
    public Animator animator;
    public float shootTimeout;
    public AudioSource shootSound;
    // public GameObject childObject;

    private bool canShoot = true;
    // private SpriteRenderer childSpriteRenderer;
    // private Animator childAnimator;


    // Start is called before the first frame update
    void Start()
    {
        shootTimeout = PlayerPrefs.GetFloat("AttackSpeed");
        animator = GetComponent<Animator>();
        // childSpriteRenderer = childObject.GetComponentInChildren<SpriteRenderer>();
        // childAnimator = childObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.RightArrow) && canShoot)
        {
            animator.SetTrigger("shoot");
            shootSound.Play();
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            StartCoroutine(ShootCooldown());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)  && canShoot)
        {
            flip();
            animator.SetTrigger("shoot");
            shootSound.Play();
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            Invoke("flip", 0.1f);
            StartCoroutine(ShootCooldown());
        }
    }

    private void flip()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.Rotate(0f, 180f, 0f);
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSecondsRealtime(shootTimeout);
        canShoot = true;
    }
}
