using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [Header("All cameras")]
    [SerializeField] private List<CinemachineVirtualCamera> cams;
    [SerializeField] private int startCamIndex = 0;
    
    void Start()
    {
        SetPriority(startCamIndex);
    }

    public void SetPriority(int camIndex)
    {
        for( int i = 0; i < cams.Count; i++)
        {
            if (i == camIndex)
            {
                cams[i].Priority = 1;
            }
            else 
            {
                cams[i].Priority = 0;
            }
        }
    }
}
