using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public Camera cam;

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

    // get movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // get rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // get rotation vector
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    // perform movement based on velocity
    private void PerformMovement()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // perform rotation
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
