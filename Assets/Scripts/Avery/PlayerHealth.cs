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
            if (health > 0)
            {
                DecreaseMeter(healAmount);
            }
            else
            {
                health = 0;
                startHealing = false;
            }
        }
    }

    private void Start()
    {
        health = 0;
        UpdateHealthBar(health, maxHealth);
        startHealing = false;
    }

    public void IncreaseMeter(float gainedMeter)
    {
        startHealing = false;
        health += gainedMeter;
        UpdateHealthBar(health, maxHealth);

        if (health >= maxHealth)
        {
            GameStateManager.GameOver();
        }
    }

    public void DecreaseMeter(float gainedHealth)
    {
        health -= gainedHealth;
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
