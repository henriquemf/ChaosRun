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
    int maxJump;

    //Touch Variables
    public int swipeThreshold = 200;
    public float swipeTimeThreshold = 0.2f;
    private Vector2 startPosition;
    private float startTime;
    private bool checkTouch = true;

    private void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        health = PlayerPrefs.GetInt("Health");
        moveSpeed = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        maxJump = 2;
        lastPosition = GetComponent<Transform>().position;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					startPosition = touch.position;
					startTime = Time.time;
					break;

				case TouchPhase.Moved:
					Vector2 swipeDelta = touch.position - startPosition;
					float swipeDistance = swipeDelta.y;

					if (Mathf.Abs(swipeDistance) > swipeThreshold)
                    {
                        float swipeTime = Time.time - startTime;

                        if (swipeDistance > 0 && swipeTime < swipeTimeThreshold)
                        {
                            if (isGrounded)
                            {
                                Jump();
                            }
                            else if (maxJump > 0 && checkTouch)
                            {
                                maxJump--;
                                Jump();
                                checkTouch = false;
                            }
                        }
                        else if (swipeDistance < 0 && swipeTime < swipeTimeThreshold && isGrounded && checkTouch)
                        {
                            Crouch();
                            checkTouch = false;
                        }
                    }

					break;

                case TouchPhase.Ended:
                    checkTouch = true;
                    break;
			}
		}
        
        if (isGrounded)
        {
            maxJump = 2;
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
