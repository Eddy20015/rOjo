using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_off_trigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
 
    
    private void Start()
    {
        foreach (GameObject obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject obj in objects)
        {
            if (obj.activeInHierarchy)
            {
    

                obj.SetActive(false);

            }
            /*
            else if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
            }
            */
            
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeInHierarchy)
            {


                obj.SetActive(false);

            }

            else if (obj.activeInHierarchy)
            {
                obj.SetActive(true);
            }
        }
    }
    */

        }
