using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_PlayerTP : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = spawnpoint.transform.position;
    }
}
