using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCollectable : MonoBehaviour
{
    // Adding a disabled timer to the collectable
    public float disabledTimer = 5f;
    public bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set the disabled timer to 5 seconds
        disabledTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        // If the collectable is disabled, start the timer
        if (disabled)
        {
            disabledTimer -= Time.deltaTime;
        }

        // If the timer is less than 0, re-enable the collectable
        if (disabledTimer < 0)
        {
            disabled = false;
            disabledTimer = 5f;
        }

        // If the collectable is disabled, disable the collider and sprite renderer
        if (disabled)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();
        if (controller != null)
        {
            controller.EnableDoubleJump();
            // Disable the collectable
            disabled = true;
        }
    }
}
