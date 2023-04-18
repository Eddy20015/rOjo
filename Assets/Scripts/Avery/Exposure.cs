using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposure : MonoBehaviour
{
    [SerializeField] float damageAmount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            collision.gameObject.GetComponent<PlayerHealth>().DecreaseHealth(damageAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HealAfterDelay(collision));
        }
    }

    public IEnumerator HealAfterDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(collision.gameObject.GetComponent<PlayerHealth>().GetHealDelay());
        collision.gameObject.GetComponent<PlayerHealth>().ChangeStartHealingVar(true);
    }
}
