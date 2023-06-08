using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : SceneTransition
{
    [Header("UI")]
    [SerializeField] private Slider slider;

    public override void UpdateUI(float progress)
    {
        slider.value = progress;
    }
}
