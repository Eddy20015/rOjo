using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposure : MonoBehaviour
{
    [SerializeField] float fillAmount;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLUS ONE EXPOSURE!");
            StopAllCoroutines();
            collision.gameObject.GetComponent<PlayerHealth>().IncreaseMeter(fillAmount);
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DecreaseAfterDelay(collision));
        }
    }

    public IEnumerator DecreaseAfterDelay(Collider collision)
    {
        yield return new WaitForSeconds(collision.gameObject.GetComponent<PlayerHealth>().GetExposureDecreaseDelay());
        collision.gameObject.GetComponent<PlayerHealth>().ChangeStartDecreasingVar(true);
    }
}
