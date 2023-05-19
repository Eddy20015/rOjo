using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Tilt : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private int time;

    [SerializeField]
    private int RotationSpeed;

    private bool camera_bool;

    private bool camera_back;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dancer"))
        {
            camera_bool = true;
            StartCoroutine("Timer");
        }
    }

    private void Update()
    {
        if (camera.transform.localEulerAngles.z <= 90 && camera.transform.localEulerAngles.z >= 0 && camera_bool)
        {
            camera.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);

            if (camera.transform.localEulerAngles.z > 90 || camera.transform.localEulerAngles.z < 0)
            {
                float y = camera.transform.rotation.eulerAngles.z;
                y = y - 90;
                camera.transform.Rotate(0, 0, -y);
            }

            Physics2D.gravity = new Vector2(Mathf.Lerp(0f, 9.81f, 9.81f), Mathf.Lerp(-2f, 0f, 0f));

        }
        else
        {
            camera_bool = false;
        }

        if (camera_back)
        {
            camera.transform.Rotate(Vector3.back, RotationSpeed * Time.deltaTime);

            if (camera.transform.localEulerAngles.z > 90 || camera.transform.localEulerAngles.z < 0)
            {
                float y = camera.transform.rotation.eulerAngles.z;
                camera.transform.Rotate(0, 0, -y);
            }

            Physics2D.gravity = new Vector2(Mathf.Lerp(0f, 9.81f, 0f), Mathf.Lerp(-2f, 0f, -2f));
        }

    }

    IEnumerator Timer()
    {
        if (time > 0)
        {
            yield return new WaitForSeconds(time);
            camera_back = true;
        }
    }
}
