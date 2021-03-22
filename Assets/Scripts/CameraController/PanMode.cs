using System;
using UnityEngine;

public class PanMode : MonoBehaviour
{
    public Boolean mirroredMovement = true;
    Vector2 firstPosition;
    Vector2 screenPosition;
    Vector2 distancePoints;
    Vector3 startPosition;
    Vector3 newPosition;
    bool firstTime = true;

    void Update()
    {
        if (Input.GetMouseButton(2)) PanMouse();

        if (Input.touchCount == 2) PanTouch();

        if (Input.GetMouseButtonUp(2)) firstTime = true;

    }
    public void PanMouse()
    {
        if (firstTime)
        {
            startPosition = transform.position;
            firstPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            firstTime = false;
        }

        screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        distancePoints = screenPosition - firstPosition;

        panUpdate(startPosition, distancePoints, 500.0f);
    }

    public void PanTouch()
    {
        Touch touchOne = Input.GetTouch(0);
        Touch touchTwo = Input.GetTouch(1);

        if (firstTime)
        { 
            startPosition = touchOne.position;
            firstTime = false;
        }

        screenPosition = touchTwo.position;

        distancePoints = firstPosition - screenPosition;

        panUpdate(startPosition, distancePoints, 2000.0f);

    }

    void panUpdate(Vector2 startPosition, Vector2 distacePoints, float divideFactor)
    {
        if (mirroredMovement)
            newPosition = new Vector3(startPosition.x - distancePoints.x / divideFactor, startPosition.y - distancePoints.y / divideFactor, 0);
        else
            newPosition = new Vector3(startPosition.x + distancePoints.x / divideFactor, startPosition.y + distancePoints.y / divideFactor, 0);

        transform.position = newPosition;
    }
}
