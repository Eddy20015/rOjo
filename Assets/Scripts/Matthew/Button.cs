using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private bool button;

    [SerializeField]
    private bool camera_tilt;

    [SerializeField]
    private int time;

    [SerializeField]
    private bool timed;

    public int RotationSpeed = 1;

    public Vector3 startRotation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && button)
        {
            door.SetActive(false);
            StartCoroutine("Timer");
        }
        else
        {
            door.SetActive(true);
        }

        if (other.gameObject.CompareTag("Player") && camera_tilt)
        {
            camera.transform.eulerAngles = startRotation;
            camera.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
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
