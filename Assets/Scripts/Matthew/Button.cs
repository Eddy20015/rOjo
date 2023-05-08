using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private int time;

    [SerializeField]
    private bool timed;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(false);
            StartCoroutine("Timer");
        }
        else
        {
            door.SetActive(true);
        }
    }

    IEnumerator Timer()
    {
        if (timed && time != 0)
        {
            yield return new WaitForSeconds(time);
            door.SetActive(true);
        }
        
    }

    /*
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(true);
        }
    }
    */
}
