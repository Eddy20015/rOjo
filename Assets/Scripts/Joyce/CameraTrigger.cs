using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private CinemachineSwitcher switcher;
    [SerializeField] private int newCamIndex = 1;
    [SerializeField] private float transitionTime = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            switcher.SetPriority(newCamIndex);
            mainCam.orthographic = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            switcher.SetPriority(0);
            mainCam.orthographic = true;
        }
    }
}
