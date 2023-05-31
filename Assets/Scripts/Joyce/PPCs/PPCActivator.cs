using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPCActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> onControllers;
    [SerializeField] private List<GameObject> offControllers;

    private void Awake()
    {
        foreach(GameObject p in onControllers)
        {
            p.SetActive(true);
        }
        foreach (GameObject p in offControllers)
        {
            p.SetActive(false);
        }
    }
}
