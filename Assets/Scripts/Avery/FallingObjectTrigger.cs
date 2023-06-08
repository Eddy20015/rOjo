using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] AK.Wwise.Event playBranches;
    bool playedSound;
    

    private void Start()
    {
        playedSound = false;
        foreach (GameObject obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            obj.AddComponent<BranchSounds>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dancer"))
        {
            foreach (GameObject obj in objects)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1f;
                print("here");
                if (!playedSound) {
                    playBranches.Post(gameObject);
                    playedSound = true;
                }
            }
        }
        
    }
}
