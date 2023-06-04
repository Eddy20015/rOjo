using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KeyFader : MonoBehaviour
{
    [SerializeField] private Material keyerStart;
    [SerializeField] private Material keyerEnd;

    [SerializeField] private float fadeTime;
    [SerializeField] private float speed = 1;
    private Material newMat;
    private float timeElapsed;

    private void Awake()
    {
        timeElapsed = 0;
        newMat = new Material(Shader.Find("Unlit/ChromaKey"));
        newMat.CopyPropertiesFromMaterial(keyerStart);
    }

    private void FixedUpdate()
    {
        if (timeElapsed <= fadeTime)
        {
            float lerp = Mathf.PingPong(timeElapsed / speed, fadeTime);
            keyerStart.Lerp(keyerStart, keyerEnd, lerp / fadeTime);
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        keyerStart.CopyPropertiesFromMaterial(newMat);
    }
}
