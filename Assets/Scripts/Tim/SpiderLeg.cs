using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    [SerializeField] Transform foot;

    [SerializeField] float angleOffset;

    [SerializeField] Vector2 initialFootPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        if (Physics2D.Raycast(transform.position, transform.right))
        {

        }
    }
}
