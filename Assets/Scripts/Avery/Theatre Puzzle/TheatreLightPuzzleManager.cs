using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreLightPuzzleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> allLights;
    [SerializeField] List<GameObject> allButtons;
    [SerializeField] GameObject door;
    bool[] initialLightStates;
    bool[] currentLightStates;
    int numOfLights;

    private void Start()
    {
        numOfLights = allLights.Count;
        initialLightStates = new bool[numOfLights];
        currentLightStates = new bool[numOfLights];
        int i = 0;

        foreach (GameObject b in allLights)
        {
            initialLightStates[i] = b.activeInHierarchy;
            i++;
        }

        initialLightStates.CopyTo(currentLightStates, 0);
    }

    public void UpdateCurrentLightStates()
    {
        bool allTrue = true;
        int i = 0;
        foreach (GameObject b in allLights)
        {
            currentLightStates[i] = b.activeInHierarchy;
            i++;
        }

        foreach (GameObject b in allLights)
        {
            if (!b.activeInHierarchy)
            {
                allTrue = false;
                break;
            }    
        }

        if (allTrue)
        {
            door.SetActive(false);
            print("SOLVED");

            foreach (GameObject b in allButtons)
            {
                b.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void ResetPuzzle()
    {
        int i = 0;
        foreach (GameObject b in allLights)
        {
            b.SetActive(initialLightStates[i]);
            i++;
        }
        UpdateCurrentLightStates();
    }
}
