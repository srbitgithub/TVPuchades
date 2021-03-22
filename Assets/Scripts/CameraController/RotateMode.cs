using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMode : MonoBehaviour
{
    public float speedH = 5.0f;
    public float speedV = 5.0f;

    private float yaw = 5.0f;
    private float pitch = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) /*&& Input.mousePosition.y < 1005*/)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
