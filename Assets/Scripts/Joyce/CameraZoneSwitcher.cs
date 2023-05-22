using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraZoneSwitcher : MonoBehaviour
{
    private static CinemachineSwitcher switcher;

    [SerializeField] private float size = 3f;
    [SerializeField] private float transitionTime = 1;

    private Collider2D myCollider;

    private void Awake()
    {
        if (!switcher)
            switcher = FindObjectOfType<CinemachineSwitcher>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Dancer"))
        {
            switcher.SetCurrZone(myCollider);
            if(size != switcher.currSize)
                switcher.SetCurrSize(size, transitionTime);
        }
    }
}
