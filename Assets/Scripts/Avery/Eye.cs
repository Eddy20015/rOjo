using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitRandomSeconds");
    }

    IEnumerator WaitRandomSeconds()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime + 1));
        SetVisibility();
    }

    public void SetVisibility()
    {
        if (isActive)
            DisableAllChildren();
        else
            EnableAllChildren();

        StartCoroutine("WaitRandomSeconds");
    }

    public void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        isActive = false;
    }

    public void EnableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        isActive = true;
    }
}
