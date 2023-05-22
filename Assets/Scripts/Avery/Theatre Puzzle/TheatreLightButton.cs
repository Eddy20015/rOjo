using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreLightButton : MonoBehaviour
{
    [SerializeField] List<GameObject> boundLights;
    [SerializeField] TheatreLightPuzzleManager manager;
    int netChangeInActiveLights = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            foreach (GameObject b in boundLights)
            {
                if (b.activeInHierarchy)
                {
                    b.SetActive(false);
                    netChangeInActiveLights--;
                }
                else
                {
                    b.SetActive(true);
                    netChangeInActiveLights++;
                }
            }

            manager.UpdateNumActive(netChangeInActiveLights);
        }
    }
}
