using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material material;
    private BoxCollider2D boxCollider;

    [SerializeField] private bool isFadeOut = true; //True is fadeOut, False is fadeIn.

    private float fadeValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        boxCollider = GetComponent<BoxCollider2D>();    //Uses Size of BoxCollider to fadeIn.
        //Set starting color of material.
        if(isFadeOut)
        {
            fadeValue = 1.0f; 
        }
        else
        {
            fadeValue = 0.0f;
        }

        material.SetFloat("_dissolve", fadeValue);
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Dancer")
        {
            Debug.Log(boxCollider.size.x);
            if(isFadeOut)
            {
                fadeValue = (boxCollider.transform.position.x - other.transform.position.x)/Mathf.Abs(boxCollider.size.x/4); 
            }
            else
            {
                fadeValue = 1.0f - (boxCollider.transform.position.x - other.transform.position.x)/Mathf.Abs(boxCollider.size.x/4);
            }
        }
        material.SetFloat("_dissolve", Mathf.Clamp(fadeValue, 0.0f, 1.0f));
    }
}
