using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float healAmount;
    [SerializeField] float healDelay;
    private float health;
    private bool startHealing = false;

    [SerializeField] Image healthBar;

    private void FixedUpdate()
    {
        if (startHealing)
        {
            if (health < maxHealth)
            {
                IncreaseHealth(healAmount);
            }
            else
            {
                health = maxHealth;
                startHealing = false;
            }
        }
    }

    private void Start()
    {
        health = maxHealth;
        UpdateHealthBar(health, maxHealth);
        startHealing = false;
    }

    public void DecreaseHealth(float lostHealth)
    {
        startHealing = false;
        health -= lostHealth;
        UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            GameStateManager.GameOver();
        }
    }

    public void IncreaseHealth(float gainedHealth)
    {
        health += gainedHealth;
        UpdateHealthBar(health, maxHealth);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        healthBar.fillAmount = fillAmount;
    }

    public void ChangeStartHealingVar(bool b)
    {
        startHealing = b;
    }

    public float GetHealDelay()
    {
        return healDelay;
    }
}
