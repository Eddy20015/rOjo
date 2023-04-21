using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposure : MonoBehaviour
{
    [SerializeField] float fillAmount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            collision.gameObject.GetComponent<PlayerHealth>().IncreaseMeter(fillAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DecreaseAfterDelay(collision));
        }
    }

    public IEnumerator DecreaseAfterDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(collision.gameObject.GetComponent<PlayerHealth>().GetExposureDecreaseDelay());
        collision.gameObject.GetComponent<PlayerHealth>().ChangeStartDecreasingVar(true);
    }
}
