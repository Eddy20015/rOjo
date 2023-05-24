using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping : MonoBehaviour
{
    [SerializeField] Transform checkpoint;
    [SerializeField] GameObject jumpScarePlaceholder; // testing
    [SerializeField] bool jumpscare;
    TheatreLightPuzzleManager manager;



    private void Start()
    {
        manager = GetComponentInParent<TheatreLightPuzzleManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            StartCoroutine("JumpscareAndSendBack", collision.gameObject);
        }
    }

    IEnumerator JumpscareAndSendBack(GameObject go)
    {
        if(jumpscare)
        {
            //put jumpscare logic here! this is just placeholder code
            jumpScarePlaceholder.SetActive(true);
            yield return new WaitForSeconds(2f);
            jumpScarePlaceholder.SetActive(false);
            go.transform.position = checkpoint.position;
            manager.ResetPuzzle();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            go.transform.position = checkpoint.position;
        }
    }
}
