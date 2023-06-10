using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // public values are editable through game settings
    public float speed = 0.5f;
    public float ySpeed = 0.5f;
    public float rotationAmount = 1f;

    public float sensitivity = 1f;

    public float maxCameraFov = 75f; // change this to player preference in Start()
    public float minCameraFov = 25f; // change this to player preference in Start()
    private float currentFov = 75f;

    // Clamp Angles
    private float minYAngle = -30f;
    private float maxYAngle = 50f;

    private float maxFrameRate = 61f;

    // For use in mouse rotation
    private Vector2 currentRotation;

    private void Start()
    {
        currentRotation.y = 0;
        maxFrameRate = Application.targetFrameRate;
    }

    // camera movement done here
    void Update()
    {
        // Camera resets
        if (Camera.main.fieldOfView > maxCameraFov)
            Camera.main.fieldOfView = maxCameraFov;
        else if (Camera.main.fieldOfView < minCameraFov)
            Camera.main.fieldOfView = minCameraFov;

        // Declerations
        float height = 0.0f;

        // Input For Map Traverse
        float xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime * maxFrameRate;
        float zAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime * maxFrameRate;

        // Input for Camera Height
        if (Input.GetKey(KeyCode.F) && transform.position.y > 0)
        {
            height -= ySpeed * Time.deltaTime * maxFrameRate;

        }
        else if (Input.GetKey(KeyCode.R))
        {
            height += ySpeed * Time.deltaTime * maxFrameRate;
        }
        
        // camera vertical rotation
        if (Input.GetKey(KeyCode.T))
        {
            if (currentRotation.y > minYAngle)
                currentRotation.y -= rotationAmount * Time.deltaTime * maxFrameRate;

        }
        else if (Input.GetKey(KeyCode.G))
        {
            if(currentRotation.y < maxYAngle)
                currentRotation.y += rotationAmount * Time.deltaTime * maxFrameRate;
        }

        // camera horizontal rotation
        if (Input.GetKey(KeyCode.Q))
        {
            
            currentRotation.x -= rotationAmount * Time.deltaTime * maxFrameRate;

        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentRotation.x += rotationAmount * Time.deltaTime * maxFrameRate;
        }

        // camera custom fov
        if (Input.GetKey(KeyCode.X))
        {
            if(Camera.main.fieldOfView < maxCameraFov)
            {
                currentFov += 1f * Time.deltaTime * maxFrameRate;
                Camera.main.fieldOfView += 1f * Time.deltaTime * maxFrameRate;
            }
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            if (Camera.main.fieldOfView > minCameraFov)
            {
                currentFov -= 1f * Time.deltaTime * maxFrameRate;
                Camera.main.fieldOfView -= 1f * Time.deltaTime * maxFrameRate;
            }
                
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
        if (Input.GetKey(KeyCode.Space))
        {
            Camera.main.fieldOfView = minCameraFov;
        }
        else
        {
            Camera.main.fieldOfView = currentFov;
        }

        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        ForwardMove.y = 0;
        ForwardMove.Normalize();
        ForwardMove *= zAxis;

        Vector3 move = verticalMove + HorizontalMove + ForwardMove;

        transform.position += move;

    }
}
