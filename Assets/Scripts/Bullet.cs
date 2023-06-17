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
        damage = playerAttack * fireLevel + 1;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // if tag is not Player, call TakeDamage
        if (hitInfo.gameObject.tag != "Player")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else if (hitInfo.gameObject.tag == "Player")
        {
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
