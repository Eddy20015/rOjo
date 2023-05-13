using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] Transform body, platform;

    [SerializeField] float move, rotate, moveSpeed, rotateSpeed;

    float moveTime, rotateTime, width, height;

    // Start is called before the first frame update
    void Start()
    {
        width = platform.transform.localScale.x;
        height = platform.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime * moveSpeed;
        rotateTime += Time.deltaTime * rotateSpeed;

        transform.position += Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.right;

        body.transform.SetLocalPositionAndRotation(move * Mathf.Sin(moveTime) * Vector3.up,
            Quaternion.Euler(rotate * Mathf.Sin(rotateTime) * Vector3.forward));
    }
}
