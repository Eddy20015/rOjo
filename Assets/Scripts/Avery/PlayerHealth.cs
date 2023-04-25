using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxExposure;
    [SerializeField] float decreaseAmount;
    [SerializeField] float decreaseDelay;
    private float currentExposure;
    private bool startDecreasing = false;

    [SerializeField] Image exposureBar;

    [SerializeField] Canvas gameOverMenu;

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
        startDecreasing = false;
        currentExposure += gainedMeter;
        UpdateExposureBar(currentExposure, maxExposure);

        if (currentExposure >= maxExposure)
        {
            gameOverMenu.GetComponent<GameOverMenu>().EnableAllChildren();
            GameStateManager.GameOver();
        }
    }

    public void DecreaseMeter(float gainedHealth)
    {
        currentExposure -= gainedHealth;
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
}
