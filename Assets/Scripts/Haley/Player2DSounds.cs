using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event jumpLanding;
    [SerializeField] private AK.Wwise.Event step;
    void PlayFootsteps()
    {
        step.Post(gameObject);
        //print("Step");
    }

    void PlayJumpLanding()
    {
        jumpLanding.Post(gameObject);
        //print("jump");
    }
}
