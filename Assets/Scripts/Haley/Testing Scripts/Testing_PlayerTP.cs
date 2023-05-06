using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_PlayerTP : MonoBehaviour
{
    //[SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject deathUI;

    private void Awake()
    {
        deathUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            GameStateManager.Pause();
            deathUI.SetActive(true);
        }
        //collision.gameObject.transform.position = spawnpoint.transform.position;
    }

    public void OnDeathButton()
    {
        GameStateManager.QuitToMainMenu();
    }
}
