using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
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

    // mengulang setiap pergerakan
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
           StartMove();
        }
       Rotation();
    }

    // untuk mencari vektor dari movement
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // mencari vektor dari rotation
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // mencari vektor rotation
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    // melakukan pergerakan berdasarkan velocity
    private void StartMove()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // melakukan rotasi
    private void Rotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // set rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            cameraRotation.x = currentCameraRotationX;

            // memberikan rotasi pada camera
            cam.transform.localEulerAngles = cameraRotation;
        }
    }
}
