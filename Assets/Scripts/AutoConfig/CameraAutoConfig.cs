//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraAutoConfig : MonoBehaviour
{
    Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;
        sceneCamera.clearFlags = CameraClearFlags.Color;
        sceneCamera.backgroundColor = Color.white;
        sceneCamera.usePhysicalProperties = true;
        sceneCamera.fieldOfView = 20;
    }
}
