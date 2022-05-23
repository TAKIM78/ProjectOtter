using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAndZoom : MonoBehaviour
{
    [SerializeField] float zoomOutMin, zoomOutMax, speedModifier;
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

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
