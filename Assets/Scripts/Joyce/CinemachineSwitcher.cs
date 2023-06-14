using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [Header("All cameras")]
    [SerializeField] private List<CinemachineVirtualCamera> cams;
    [SerializeField] private int startCamIndex = 0;

    [Header("Bookkeeping Vars")]
    [SerializeField] private float finalSize; // IMPORTANT FOR SMOOTH TRANSITION THAT CAMERA SIZE ENDS AT 3!!!

    public float currSize { get;  private set; }
    private int currCamIndex;
    private CinemachineConfiner confiner;

    
    void Start()
    {
        SetPriority(startCamIndex);
        currCamIndex = startCamIndex;
        currSize = cams[currCamIndex].m_Lens.OrthographicSize;
    }

    public void SetPriority(int camIndex)
    {
        currCamIndex = camIndex;
        cams[currCamIndex].TryGetComponent<CinemachineConfiner>(out confiner);

        for ( int i = 0; i < cams.Count; i++)
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

    public void ToggleAllCameras(bool enable)
    {
        foreach(CinemachineVirtualCamera cam in cams)
        {
            cam.gameObject.SetActive(enable);
        }
    }

    public void SetCurrZone(Collider2D collider)
    {
        confiner.m_BoundingShape2D = collider;
    }

    public void SetDamping(float damping = 1f)
    {
        confiner.m_Damping = damping;
    }

    public void SetCurrSize(float orthoSize, float transitionTime = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(LerpCamSize(orthoSize, transitionTime));
    }

    private IEnumerator LerpCamSize(float newSize, float transitionTime)
    {
        CinemachineVirtualCamera currCam = cams[currCamIndex];
        float time = 0;
        while (time <= transitionTime)
        {
            currCam.m_Lens.OrthographicSize = Mathf.Lerp(currCam.m_Lens.OrthographicSize, newSize, time / transitionTime);
            currSize = currCam.m_Lens.OrthographicSize;
            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;
        }
    }
}
