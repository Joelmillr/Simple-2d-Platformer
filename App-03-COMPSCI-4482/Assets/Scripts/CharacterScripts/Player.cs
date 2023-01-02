using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Players starting position
    private Vector3 startPosition;

    // Players rigidbody, character controller, and health
    public Rigidbody2D rb;
    public CharacterController2D controller;
    public HealthManagement health;

    // Players animator
    public Animator animator;

    // Players movement speed
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    // Player states
    bool jump = false;
    bool crouch = false;

    // Double jump
    bool doubleJump = false;
    public float doubleJumpForce = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Get the players starting position
        startPosition = transform.position;
        // Get the players health
        health = GetComponent<HealthManagement>();
    }

    void Update()
    {
        // See if player is dead
        if (health.health == 0)
        {
            // Reset the players position
            transform.position = startPosition;
            // Reset the players health
            health.ResetHealth();
        }

        // Get input from player
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Set animation parameters
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        // Determine if player is falling or jumping
        if (controller.IsGrounded())
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else if (rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }

        // Crouch
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void EnableDoubleJump()
    {
        // remove y velocity
        rb.velocity = new Vector2(rb.velocity.x, 0);
        // add force
        rb.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
    }

    public void Push(bool direction)
    {
        // remove x velocity
        rb.velocity = new Vector2(0, rb.velocity.y);
        // add force
        bool facingRight = transform.localScale.x > 0;
        if (direction == facingRight)
        {
            rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        }
    }

    public void ResetPlayer()
    {
        // Reset the players position
        transform.position = startPosition;
    }

    public void softResetPlayer()
    {
        // Reset the players position
        transform.position = startPosition;
        // Reset the players health
        health.ResetHealth();
    }
}
