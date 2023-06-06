using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSoundFix : MonoBehaviour
{
    [SerializeField] GameObject spider;

    // Start is called before the first frame update
    void Start()
    {
        spider.SetActive(false);
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1);
        spider.SetActive(true);
    }
}
