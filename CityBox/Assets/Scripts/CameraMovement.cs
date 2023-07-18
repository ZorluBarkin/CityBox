/*  
 * Copyright 2023 Barkın Zorlu 
 * All rights reserved.
 * 
 * This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
 * To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraMovement : MonoBehaviour
{
    public Camera cam = null;

    // public values are editable through game settings
    public float speed = 0.5f;
    public float verticalSpeed = 0.5f;
    public float rotationAmount = 1f;

    public float sensitivity = 1f;

    // these are vertical FOV values
    public float maxCameraFov = 60f; // change this to player preference in Start()
    public float minCameraFov = 15f; // change this to player preference in Start()
    private float currentFov = 50f;

    // Clamp Angles
    private float minYAngle = -30f;
    private float maxYAngle = 50f;

    private float maxFrameRate = 61f;

    // For use in mouse rotation
    private Vector2 currentRotation;

    private void Start()
    {
        
        if(cam == null)
            cam = Camera.main;

        // Get values from Settings
        maxCameraFov = Camera.HorizontalToVerticalFieldOfView(Settings._MaxHFOV, cam.aspect);
        minCameraFov = Camera.HorizontalToVerticalFieldOfView(Settings._MinHFOV, cam.aspect);

        currentRotation.y = 0;
        maxFrameRate = Application.targetFrameRate + 1;
    }

    // camera movement done here
    void Update()
    {
        //float Hfov = Camera.VerticalToHorizontalFieldOfView(cam.fieldOfView, cam.aspect);
        // Camera resets
        if (cam.fieldOfView > maxCameraFov)
            cam.fieldOfView = maxCameraFov;
        else if (cam.fieldOfView < minCameraFov)
            cam.fieldOfView = minCameraFov;

        // Declerations
        float height = 0.0f;

        // Input For Map Traverse
        float xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime * maxFrameRate;
        float zAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime * maxFrameRate;

        // Input for Camera Height
        if (Input.GetKey(KeyCode.F) && transform.position.y > 0)
        {
            height -= verticalSpeed * Time.deltaTime * maxFrameRate;

        }
        else if (Input.GetKey(KeyCode.R))
        {
            height += verticalSpeed * Time.deltaTime * maxFrameRate;
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
            if(cam.fieldOfView < maxCameraFov)
            {
                currentFov += 1f * Time.deltaTime * maxFrameRate;
                cam.fieldOfView += 1f * Time.deltaTime * maxFrameRate;
            }
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            if (cam.fieldOfView > minCameraFov)
            {
                currentFov -= 1f * Time.deltaTime * maxFrameRate;
                cam.fieldOfView -= 1f * Time.deltaTime * maxFrameRate;
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
            cam.fieldOfView = minCameraFov;
        }
        else
        {
            cam.fieldOfView = currentFov;
        }

        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        ForwardMove.y = 0;
        ForwardMove.Normalize();
        ForwardMove *= zAxis;

        Vector3 move = verticalMove + HorizontalMove + ForwardMove;

        transform.position += move;

    }

}
