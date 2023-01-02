using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;

    Path path;

    int currentWaypoint = 0;

    public float nextWaypointDistance = 3f;

    bool reachedEndOfPath = false;

    Seeker seeker;

    Rigidbody2D rb;

    HealthManagement healthManagement;

    public HealthBar healthBar;

    private bool cherries = true;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        healthManagement = GetComponent<HealthManagement>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            // Determine if enemy is at half health
            float current_health = healthManagement.health;
            float maxHealth = healthManagement.maxHealth;
            float ratio = current_health / maxHealth;

            // if enemy is at 3/4th health, increase speed
            if (ratio <= 0.75f)
            {
                speed = 500f;
            }

            // If enemy is at half health, go to cherries
            if (cherries && ratio <= 0.5)
            {
                // Get the transform of the health collectable closest to the enemy
                GameObject healthCollectable = GameObject.Find("EnemyCherry");
                if (healthCollectable != null)
                {
                    Transform health = healthCollectable.transform;
                    seeker.StartPath(rb.position, health.position, OnPathComplete);
                }
                else
                {
                    cherries = false;
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                }
            }
            else
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction =
            ((Vector2) path.vectorPath[currentWaypoint] - rb.position)
                .normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce (force);

        float distance =
            Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            // Health bar should not flip
            Vector3 healthBarScale = healthBar.transform.localScale;
            healthBarScale.x = -Mathf.Abs(healthBarScale.x);
            healthBar.transform.localScale = healthBarScale;
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            // Health bar should not flip
            Vector3 healthBarScale = healthBar.transform.localScale;
            healthBarScale.x = Mathf.Abs(healthBarScale.x);
            healthBar.transform.localScale = healthBarScale;
        }
    }
}
