using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MatchPos : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        transform.position = target.position;
    }
    private void Update()
    {
        transform.position = target.position;
    }
}
