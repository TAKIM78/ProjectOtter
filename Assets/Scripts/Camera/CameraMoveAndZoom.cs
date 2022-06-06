using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAndZoom : MonoBehaviour
{
    [SerializeField] float zoomOutMin, zoomOutMax;
    [SerializeField] GameObject lastRoom;
    private float speedModifier = 0.02f;
    private Camera cameraMain;
    private Touch touch;

    private void Start()
    {
        cameraMain = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
        FixSpeedModifier();
    }

    private void MoveCamera()
    {
        Vector3 fixPosition = Vector3.zero;
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
                fixPosition = new Vector3(-touch.deltaPosition.x * speedModifier, -touch.deltaPosition.y * speedModifier, 0f);
            gameObject.transform.position += fixPosition;
            Camera.main.transform.position = ClampCamera(Camera.main.transform.position);
        }
    }

    private void ZoomCamera()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitute = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float difference = currentMagnitude - prevMagnitute;

            Zoom(difference * speedModifier);
        }
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        Camera.main.transform.position = ClampCamera(Camera.main.transform.position);
    }

    float tempSpeedMod = 0;
    private void FixSpeedModifier()
    {
        if (speedModifier != 0.01f && Camera.main.orthographicSize < 6)
        {
            speedModifier = 0.01f;
            tempSpeedMod = speedModifier;
        }
        else if(speedModifier!= 0.02f && Camera.main.orthographicSize >= 6)
        {
            speedModifier = 0.02f;
            tempSpeedMod = speedModifier;
        }
    }

    private Vector3 ClampCamera(Vector3 targetPos)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minX = 0f + camWidth;
        float maxX = 100f - camWidth;
        float minY = -17 + camHeight;
        float maxY = 31 - camHeight;

        if (lastRoom.GetComponent<Room>().isBuilt)
            maxX = 115f - camWidth;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(newX, newY, targetPos.z);
    }
}
