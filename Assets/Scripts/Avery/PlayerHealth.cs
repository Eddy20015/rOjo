using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxExposure;
    [SerializeField] float decreaseAmount;
    [SerializeField] float decreaseDelay;
    public float currentExposure { get; private set; }
    private bool startDecreasing = false;

    [SerializeField] Image exposureBar;

    [SerializeField] EndingUI gameOverMenu;

    [SerializeField] private bool showBar;

    private bool isDead = false;

    private void FixedUpdate()
    {
        if (startDecreasing)
        {
            if (currentExposure > 0)
            {
                DecreaseMeter(decreaseAmount);
            }
            else
            {
                currentExposure = 0;
                startDecreasing = false;
            }
        }
    }

    private void Start()
    {
        currentExposure = 0;
        UpdateExposureBar(currentExposure, maxExposure);
        startDecreasing = false;
    }

    public void IncreaseMeter(float gainedMeter)
    {
        if (currentExposure < maxExposure) {
            startDecreasing = false;
            currentExposure += gainedMeter;
            AkSoundEngine.SetRTPCValue("Player_Health", (currentExposure / maxExposure * 100));
            if (showBar)
                UpdateExposureBar(currentExposure, maxExposure);
        }

        if (currentExposure >= maxExposure && !isDead)
        {
            isDead = true;
            gameOverMenu.PlayMenu();
            //GameStateManager.GameOver();
        }
    }

    public void DecreaseMeter(float gainedHealth)
    {
        currentExposure -= gainedHealth;
        if (showBar)
            UpdateExposureBar(currentExposure, maxExposure);
    }

    public void UpdateExposureBar(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        exposureBar.fillAmount = fillAmount;
    }

    public void ChangeStartDecreasingVar(bool b)
    {
        startDecreasing = b;
    }

    public float GetExposureDecreaseDelay()
    {
        return decreaseDelay;
    }

    public float GetExposure()
    {
        return currentExposure / maxExposure; 
    }

    private void OnEnable()
    {
        GameStateManager.Restarted += OnRestart;
    }

    private void OnDisable()
    {
        GameStateManager.Restarted -= OnRestart;
    }

    public void OnRestart()
    {
        isDead = false;
        currentExposure = 0;
    }
}
