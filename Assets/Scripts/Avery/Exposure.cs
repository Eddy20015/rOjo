using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposure : MonoBehaviour
{
    [SerializeField] float fillAmount;
    private bool playerInBeam = false;

    private void FixedUpdate()
    {
        if (playerInBeam)
            GameObject.FindGameObjectWithTag("Dancer").gameObject.GetComponent<PlayerHealth>().IncreaseMeter(fillAmount);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            StopAllCoroutines();
            playerInBeam = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            playerInBeam = false;
            StartCoroutine(DecreaseAfterDelay(collision));
        }
    }

    public IEnumerator DecreaseAfterDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(collision.gameObject.GetComponent<PlayerHealth>().GetExposureDecreaseDelay());
        collision.gameObject.GetComponent<PlayerHealth>().ChangeStartDecreasingVar(true);
    }
}
