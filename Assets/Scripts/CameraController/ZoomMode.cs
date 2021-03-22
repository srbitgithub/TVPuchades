using UnityEngine;
using UnityEngine.UI;

public class ZoomMode : MonoBehaviour
{
    public Slider zoomSlider;
    float zoomMax;
    float zoomMin;
    Camera sceneCamera;

    void Start()
    {
        zoomMin = zoomSlider.minValue;
        zoomMax = zoomSlider.maxValue;
        sceneCamera = Camera.main;
        zoomSlider.value = zoomMax - sceneCamera.fieldOfView;
    }

    private void Update()
    {
        zoomMouse(Input.GetAxis("Mouse ScrollWheel") * -1, 25.0f);
    }

    void zoomMouse(float increment, float scale)
    {
        if (increment != 0)
        {
            sceneCamera.fieldOfView += increment * scale;
            sceneCamera.fieldOfView = Mathf.Clamp(sceneCamera.fieldOfView, zoomMin, zoomMax);
        }
    }

    public void zoomTouch()
    {
        Camera.main.fieldOfView = zoomMax - zoomSlider.value;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, zoomMin, zoomMax);
    }   
}
