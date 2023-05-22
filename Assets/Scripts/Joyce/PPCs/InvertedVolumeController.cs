using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InvertedVolumeController : PostProcessController
{
    [SerializeField] private GameObject volume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Dancer"))
        {
            ToggleEffect(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Dancer"))
        {
            ToggleEffect(false);
        }
    }

    public override void ToggleEffect(bool on)
    {
        volume.SetActive(on);
    }


}
