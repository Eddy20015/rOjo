using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetect : MonoBehaviour
{
    [SerializeField] private Player2DMovement player;
    public bool onSlope{ get; private set;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Slope"))
        {
            print("on slope");
            onSlope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Slope"))
        {
            print("off slope");
            onSlope = false;
        }
    }
}
