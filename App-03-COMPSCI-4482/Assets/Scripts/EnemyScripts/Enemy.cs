using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthManagement enemyHealthManagement;

    private Transform enemyTransform;

    private int damage = -1;

    public Bushes bushes;

    public bool enemyMad = false;

    void Start()
    {
        enemyHealthManagement = GetComponent<HealthManagement>();
        enemyTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // See if enemy is dead
        if (enemyHealthManagement.health == 0)
        {
            // Destroy the enemy
            Destroy (gameObject);

            // Remove the bushes
            if (bushes != null)
            {
                bushes.DisableBushes();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player playerController = other.GetComponent<Player>();
        HealthManagement playerHealth = other.GetComponent<HealthManagement>();
        if (playerController != null)
        {
            // Player should bounce off enemy
            playerController.EnableDoubleJump();

            // player should be pushed off enemy
            playerController.Push(true);

            // Player should not take damage when they are bouncing on the enemy
            playerHealth.MakeInvincible(0.5f);

            // Enemy should take damage
            enemyHealthManagement.ChangeHealth (damage);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        HealthManagement controller =
            other.gameObject.GetComponent<HealthManagement>();
        Player playerController = other.gameObject.GetComponent<Player>();

        // Grab the players position
        Vector3 playerPosition = other.gameObject.transform.position;

        if (controller != null)
        {
            controller.ChangeHealth (damage);
            playerController.Push(true);
        }
    }
}
