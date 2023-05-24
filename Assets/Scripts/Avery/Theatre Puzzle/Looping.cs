using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping : MonoBehaviour
{
    [SerializeField] float timeUntilSendBack = 0f;
    [SerializeField] Transform checkpoint;
    [SerializeField] Looping oppositeCheckpoint;
    [SerializeField] GameObject jumpScarePlaceholder; // testing
    TheatreLightPuzzleManager manager;
    bool justTeleported = false;
    Player2DMovement player;

    private void Start()
    {
        manager = GetComponentInParent<TheatreLightPuzzleManager>();
        player = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Player2DMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            justTeleported = true;
            StartCoroutine("SendBack", collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            oppositeCheckpoint.justTeleported = false;
        }
    }

    IEnumerator SendBack(GameObject go)
    {
        yield return new WaitForSeconds(timeUntilSendBack);
        oppositeCheckpoint.GetComponent<Collider2D>().enabled = false;

        Vector3 newPosition = go.transform.position;
        newPosition.x = checkpoint.position.x; // Set only the x coordinate
        go.transform.position = newPosition;

        yield return new WaitForSeconds(.3f);
        oppositeCheckpoint.GetComponent<Collider2D>().enabled = true;

        manager.ResetPuzzle();
    }
}
