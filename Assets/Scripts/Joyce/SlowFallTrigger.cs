using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFallTrigger : MonoBehaviour
{
    [SerializeField] private bool turnOn;
    private Player2DMovement player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dancer").GetComponent<Player2DMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Dancer"))
            player.SlowFall(turnOn);
    }
}
