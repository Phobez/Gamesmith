﻿using UnityEngine;

/// <summary>
/// A component to handle player movement.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public Camera cam;                              // player camera reference

    // cached variables
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private float cameraRotationX = 0.0f;
    private float currentCameraRotationX = 0.0f;

    [SerializeField]
    private float cameraRotationLimit = 85.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // run every physics iteration
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            PerformMovement();
        }
        PerformRotation();
    }

    /// <summary>
    /// A method to get movement vector.
    /// </summary>
    /// <param name="_velocity">New movement vector.</param>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// A method to get rotation vector.
    /// </summary>
    /// <param name="_rotation">New rotation vector.</param>
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    /// <summary>
    /// A method to get camera x rotation.
    /// </summary>
    /// <param name="_cameraRotationX">New camera x rotation.</param>
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    /// <summary>
    /// A method to perform movement based on velocity.
    /// </summary>
    private void PerformMovement()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    /// <summary>
    /// A method to perform player and camera rotation.
    /// </summary>
    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // set rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            cameraRotation.x = currentCameraRotationX;

            // apply rotation to transform of camera
            cam.transform.localEulerAngles = cameraRotation;
        }
    }
}
