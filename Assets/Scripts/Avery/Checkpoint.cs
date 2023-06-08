using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private static bool hasReachedAtLeastOneCheckpoint = false;

    void Start()
    {
        LoadCheckpoint();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dancer"))
        {
            checkpointPosition = collision.gameObject.transform.position;
            hasReachedAtLeastOneCheckpoint = true;
            SaveCheckpoint();
        }
    }

    public void SaveCheckpoint()
    {
        PlayerPrefs.SetFloat("checkpointX", checkpointPosition.x);
        PlayerPrefs.SetFloat("checkpointY", checkpointPosition.y);
        PlayerPrefs.SetFloat("checkpointZ", checkpointPosition.z);
        PlayerPrefs.Save();
    }

    public static void LoadCheckpoint()
    {
        float checkpointX = PlayerPrefs.GetFloat("checkpointX");
        float checkpointY = PlayerPrefs.GetFloat("checkpointY");
        float checkpointZ = PlayerPrefs.GetFloat("checkpointZ");
        Vector3 checkpointPosition = new Vector3(checkpointX, checkpointY, checkpointZ);
        
        //if (hasReachedAtLeastOneCheckpoint)
        GameObject.FindGameObjectWithTag("Dancer").transform.position = checkpointPosition;
    }
}
