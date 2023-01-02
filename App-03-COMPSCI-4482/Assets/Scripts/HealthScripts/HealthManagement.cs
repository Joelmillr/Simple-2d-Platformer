using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthManagement : MonoBehaviour
{
    // Animator variable
    public Animator animator;

    // Health variables
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    // Invincibility variables
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    // Health bar variables
    public HealthBar healthBar;

    // Sprite Renderer variable
    SpriteRenderer spriteRenderer;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            MakeInvincible(timeInvincible);
            // Play hurt animation if parameter is set
            animator.SetTrigger("IsHurt"); 
            // Flash
            StartCoroutine(Flash());
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator Flash()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    public void MakeInvincible(float time)
    {
        isInvincible = true;
        invincibleTimer = time;
    }
}
