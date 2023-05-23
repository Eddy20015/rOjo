using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private bool two_button;

    [SerializeField]
    private Button other_button;

    public bool collision;
    
    [SerializeField]
    private bool timed;
    
    [SerializeField]
    private int time;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (two_button)
        {
            if (other.gameObject.CompareTag("Dancer"))
            {
                collision = true;

                if (collision && other_button.collision)
                {
                    door.SetActive(false);
                    StartCoroutine("Timer");
                }
            }
            else
            {
                door.SetActive(true);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Dancer"))
            {
                door.SetActive(false);
                StartCoroutine("Timer");
            }
            else
            {
                door.SetActive(true);
            }
        }
    }

    IEnumerator Timer()
    {
        if (time > 0)
        {
            yield return new WaitForSeconds(time);

            door.SetActive(true);

            collision = false;
        }
    }
}
