using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeVisibilityRandomizer : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] AK.Wwise.Event playSquish;
    GameObject dancer;

    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitRandomSeconds");
        dancer = GameObject.FindGameObjectWithTag("Dancer");
    }

    IEnumerator WaitRandomSeconds()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime + 1));
        SetVisibility();
    }

    public void SetVisibility()
    {
        AkSoundEngine.SetRTPCValue("SquishEyeDistance", Vector3.Distance(dancer.transform.position, transform.position));
        print("here");
        
        playSquish.Post(gameObject);
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
