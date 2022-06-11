using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // public values are editable through game settings
    public float speed;
    public float ySpeed;

    public float sensitivity = 1f;

    public float cameraFov = 75; // change this to player preference in Start()
    public float cameraZoomFov = 30; // change this to player preference in Start()

    // Clamp Angles
    private float minYAngle = -30f;
    private float maxYAngle = 50f;

    // For use in mouse rotation
    private Vector2 currentRotation;

    private void Start()
    {
        currentRotation.y = 0;
        speed = 0.5f;
        ySpeed = 0.5f;
    }

    void FixedUpdate()
    {
        // Declerations
        float height = 0.0f;

        // Input For Map Traverse
        float xAxis = Input.GetAxis("Horizontal") * speed;
        float zAxis = Input.GetAxis("Vertical") * speed;

        // Input for Camera Height
        if (Input.GetKey(KeyCode.Q) && transform.position.y > 0)
        {
            height -= ySpeed;

        }
        else if (Input.GetKey(KeyCode.E))
        {
            height += ySpeed;
        }

        // Axis Changes
        Vector3 verticalMove = new Vector3(0, height, 0);
        Vector3 HorizontalMove = xAxis * transform.right;
        Vector3 ForwardMove = transform.forward;

        // Mouse Rotation
        if (Input.GetMouseButton(1))
        {
            // lock cursor for use
            Cursor.lockState = CursorLockMode.Locked;

            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;

            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, minYAngle, maxYAngle);

        }
        else
        {
            // Unlock the cursor after use
            Cursor.lockState = CursorLockMode.None;
        }

        // Camera Zoom via FOV Manupilation
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = cameraZoomFov;
        }
        else
        {
            Camera.main.fieldOfView = cameraFov;
        }

        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        ForwardMove.y = 0;
        ForwardMove.Normalize();
        ForwardMove *= zAxis;

        Vector3 move = verticalMove + HorizontalMove + ForwardMove;

        transform.position += move;

    }
}
