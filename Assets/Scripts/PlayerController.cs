using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float lookSensitivity = 3.0f;
    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private float gravity = 14.0f;

    // component caching
    private PlayerMover mover;

    private float xMov;
    private float zMov;
    private float yRot;
    private float xRot;
    private float cameraRotationX;
    private float verticalVelocity;
    private Vector3 movHorizontal;
    private Vector3 movVertical;
    private Vector3 velocity;
    private Vector3 rotation;
    private bool isGrounded;

    private void Start()
    {
        mover = GetComponent<PlayerMover>();

        rotation = Vector3.zero;
        cameraRotationX = 0.0f;
    }

    private void Update()
    {
        // calculate movement velocity as 3D vector
        xMov = Input.GetAxis("Horizontal");
        zMov = Input.GetAxis("Vertical");

        movHorizontal = transform.right * xMov;
        movVertical = transform.forward * zMov;

        if (isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                isGrounded = false;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // final movement vector
        velocity = (movHorizontal + movVertical).normalized * speed;
        velocity.y = verticalVelocity;

        // apply movement
        mover.Move(velocity);

        // calculate rotation as 3D vector (turning around)
        yRot = Input.GetAxisRaw("Mouse X");

        rotation.Set(0.0f, yRot, 0.0f);
        rotation *= lookSensitivity;

        // apply rotation
        mover.Rotate(rotation);

        // calculate camera rotation as 3D vector (turning around)
        xRot = Input.GetAxisRaw("Mouse Y");

        cameraRotationX = xRot * lookSensitivity;

        // apply rotation
        mover.RotateCamera(cameraRotationX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
