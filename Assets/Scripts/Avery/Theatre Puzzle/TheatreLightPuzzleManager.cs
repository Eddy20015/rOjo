using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreLightPuzzleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> allLights;
    [SerializeField] GameObject door;
    int numOfLights;
    int numActive = 0;

    private void Start()
    {
        numOfLights = allLights.Count;

        foreach (GameObject b in allLights)
        {
            if (b.activeInHierarchy)
                numActive++;
        }
        print(numActive);
    }

    public void UpdateNumActive(int i)
    {
        numActive += i;

        if (numActive == numOfLights)
            door.SetActive(false);
    }
}
