using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private int damage;
    private int fireLevel;
    private int playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = PlayerPrefs.GetInt("AttackPoints");
        fireLevel = StatsManager.playerShootLevel;
        damage = playerAttack * fireLevel;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        fireLevel = StatsManager.playerShootLevel;
        damage = playerAttack * fireLevel;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
