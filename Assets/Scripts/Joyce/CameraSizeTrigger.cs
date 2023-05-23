using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeTrigger : MonoBehaviour
{
    private static CinemachineSwitcher switcher;

    [SerializeField] private float size = 3f;
    [SerializeField] private float transitionTime = 1;

    private void Awake()
    {
        if (!switcher)
            switcher = FindObjectOfType<CinemachineSwitcher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Dancer"))
        {
            switcher.SetCurrSize(size, transitionTime);
        }
    }
}
