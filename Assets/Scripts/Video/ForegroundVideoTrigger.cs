using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundVideoTrigger : MonoBehaviour
{
    private ForegroundController foregroundController;

    [SerializeField] bool PlayHands1;
    [SerializeField] bool PlayHands2;
    [SerializeField] bool PlayHands3;
    [SerializeField] bool PlayHands4;

    private void Start()
    {
        foregroundController = FindObjectOfType<ForegroundController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dancer")
        {
            foregroundController.StopForeground();

            // see if I can make multiple textures so multiple hands can play at once?
            if(PlayHands1 || PlayHands2 || PlayHands3)
            {
                if(PlayHands1)
                {
                    foregroundController.PlayNewHandVideo(1);
                }
                else if(PlayHands2)
                {
                    foregroundController.PlayNewHandVideo(2);
                }
                else if(PlayHands3)
                {
                    foregroundController.PlayNewHandVideo(3);
                }
                else if(PlayHands4)
                {
                    foregroundController.PlayNewHandVideo(4);
                }
            }
            else
            {
                foregroundController.PlayNewForeground();
            }
         
            GetComponent<Collider2D>().enabled = false;
        }
    }
}