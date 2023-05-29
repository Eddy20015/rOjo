using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField] Transform leftTeleportationPoint;
    [SerializeField] Transform rightTeleportationPoint;
    GameObject player;
    TheatreLightPuzzleManager manager;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Dancer");
        manager = GetComponentInParent<TheatreLightPuzzleManager>();
    }

    private void Update()
    {
        // Teleport the player to the appropriate teleportation point
        if (player.transform.position.x < leftTeleportationPoint.position.x)
        {
            print("Passing left checkpoint");
            Vector3 newPosition = player.transform.position;
            newPosition.x = rightTeleportationPoint.position.x;
            player.transform.position = newPosition;
            manager.ResetPuzzle();
        }
        else if (player.transform.position.x > rightTeleportationPoint.position.x)
        {
            print("Passing right checkpoint");
            Vector3 newPosition = player.transform.position;
            newPosition.x = leftTeleportationPoint.position.x;
            player.transform.position = newPosition;
            manager.ResetPuzzle();
        }
    }
}
