using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreLightButton : MonoBehaviour
{
    [SerializeField] List<GameObject> boundLights;
    [SerializeField] TheatreLightPuzzleManager manager;
    [SerializeField] bool isResetButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer") && !isResetButton)
        {
            foreach (GameObject b in boundLights)
            {
                if (b.activeInHierarchy)
                {
                    b.SetActive(false);
                }
                else
                {
                    b.SetActive(true);
                }
            }

            manager.UpdateCurrentLightStates();
        }
        else if (collision.gameObject.CompareTag("Dancer") && isResetButton)
        {
            manager.ResetPuzzle();
        }
    }
}
