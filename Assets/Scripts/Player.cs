using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Animator animator;

	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .96f;	

    private float health;
    public float JumpSpeed;
    public static float moveSpeed;
    public static float DistanceTravelled = 0f;
    public static float position;
    public static float positionX;

    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    public bool isGrounded;

    public GameObject gameOverPanel;

    public AudioSource hurtSound;
    
    private Vector3 lastPosition;
    private Rigidbody2D rb;
    private CameraShake cameraShake;
    
    //Extra Jump
    public int maxJumpValue;
    int maxJump;

    private void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        health = PlayerPrefs.GetInt("Health");
        moveSpeed = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        maxJump = maxJumpValue;
        lastPosition = GetComponent<Transform>().position;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        if (Input.GetKeyDown(KeyCode.UpArrow) && maxJump > 0)
        {
            maxJump--;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && maxJump == 0 && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            Crouch();
        }
        
        if (isGrounded)
        {
            maxJump = maxJumpValue;
        }

        animator.SetBool("isWalking", true);
		animator.SetBool("isFalling", rb.velocity.y < 0);
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        float distance = transform.position.x - lastPosition.x;
        if (distance > 0)
        {
            DistanceTravelled += distance;
        }

        lastPosition = GetComponent<Transform>().position;
        position = transform.position.x;
    }

    void Jump()
    {
        animator.SetTrigger("jump");
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, JumpSpeed));
    }

    void Crouch()
    {
        animator.SetTrigger("roll");
        rb.velocity = new Vector2(moveSpeed * m_CrouchSpeed, rb.velocity.y);
    }

    // Adicione um getter para a velocidade atual do jogador
    public float CurrentSpeed { get { return rb.velocity.x; } }

    public void TakeDamage (float damage)
    {
        health = health - damage;
        Debug.Log("Current PLAYER health: " + health);
        StartCoroutine(cameraShake.Shake(.3f, .3f));
        hurtSound.Play();
        animator.SetTrigger("hit");
        if (health <= 0)
        {
            animator.SetTrigger("die");
            Invoke("DestroyObject", 1.0f);
            gameOverPanel.SetActive(true);
            PauseManager.Pause();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
