using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;

	Rigidbody2D m_Rigidbody2D;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	void Start()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetBool("isWalking", horizontalMove != 0 && controller.m_Grounded);
		animator.SetBool("isFalling", m_Rigidbody2D.velocity.y < 0);

		if (Input.GetButtonDown("Jump"))
		{
			animator.SetTrigger("jump");
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			animator.SetTrigger("roll");
			crouch = true;
		} 
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

	}

	void FixedUpdate ()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}