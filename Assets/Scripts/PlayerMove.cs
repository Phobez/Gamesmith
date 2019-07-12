using UnityEngine;

// Written by : Hans Budiman
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 rotCam = Vector3.zero;
    private float rotCamX = 0.0f;
    private float currentRotCamX = 0.0f;

    [SerializeField]
    private float rotLimit = 85.0f;
    [SerializeField]
    private float crouchCamHeight = 0.5f;
    [SerializeField]
    private float standCamHeight = 1.0f;

    private Rigidbody rb;
    private Entity ent;

    private Vector3 camLocalPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ent = GetComponent<Entity>();

        camLocalPosition = cam.transform.localPosition;
    }

    private void Update()
    {
        if (ent.IsCrouching)
        {
            if (cam.transform.localPosition.y > crouchCamHeight)
            {
                if (cam.transform.localPosition.y - (ent.crouchDeltaHeight * Time.deltaTime * 8) < crouchCamHeight)
                {
                    camLocalPosition.y = crouchCamHeight;
                    cam.transform.localPosition = camLocalPosition;
                }
                else
                {
                    camLocalPosition.y -= ent.crouchDeltaHeight * Time.deltaTime * 8;
                    cam.transform.localPosition = camLocalPosition;
                }
            }
        }
        else
        {
            if (cam.transform.localPosition.y < standCamHeight)
            {
                if (cam.transform.localPosition.y + (ent.crouchDeltaHeight * Time.deltaTime * 8) > standCamHeight)
                {
                    camLocalPosition.y = standCamHeight;
                    cam.transform.localPosition = camLocalPosition;
                }
                else
                {
                    camLocalPosition.y += ent.crouchDeltaHeight * Time.deltaTime * 8;
                    cam.transform.localPosition = camLocalPosition;
                }
            }
        }
    }

    // mengulang setiap pergerakan
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            PerformMove();
        }
        PerformRotation();
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
    public void RotateCamera(float _rotCamX)
    {
        rotCamX = _rotCamX;
    }

    // melakukan pergerakan berdasarkan velocity
    private void PerformMove()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // melakukan rotasi
    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // set rotation and clamp it
            currentRotCamX -= rotCamX;
            currentRotCamX = Mathf.Clamp(currentRotCamX, -rotLimit, rotLimit);
            rotCam.x = currentRotCamX;

            // memberikan rotasi pada camera
            cam.transform.localEulerAngles = rotCam;
        }
    }
}
