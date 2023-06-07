using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSounds : MonoBehaviour
{
    //[SerializeField] AK.Wwise.Event playImpact;
    Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.gravityScale > 0)
            AkSoundEngine.PostEvent("Play_Branch_Impact", gameObject);
        //playImpact.Post(gameObject);
    }
}
