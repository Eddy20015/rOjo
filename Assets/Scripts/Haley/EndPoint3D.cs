using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndPoint3D : EndingUI
{
    [Header("On Win Objects")]
    [SerializeField] private Chaser3D chaser;
    [SerializeField] private Player3DMovement pMove;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            chaser.Halt();
            pMove.enabled = false;
            PlayMenu();
        }
    }
}
