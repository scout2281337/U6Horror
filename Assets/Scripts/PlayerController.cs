using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 350;

    private Rigidbody RB;
    private float cameraVerticalAngle = 0f;

    private bool canJump = false;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float maxLookAngle = 80f;
    private float yRotation = 0f;

    // Camera bobbing
    [SerializeField] private float bobFrequency = 10f;
    [SerializeField] private float bobAmplitude = 0.05f;
    private float bobTimer = 0f;
    private Vector3 defaultCameraLocalPos;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        defaultCameraLocalPos = playerCamera.localPosition;
    }

    void Update()
    {
        PlayerMovement(speed);
        PlayerJump(jumpForce);
        MouseRotaion();
        CameraBob();
        canJump = Physics.Raycast(groundCheck.position, Vector3.down, checkRadius, groundMask);
    }

    void PlayerMovement(float MovementSpeed)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        Vector3 velocity = new Vector3(moveDirection.x * speed, RB.linearVelocity.y, moveDirection.z * speed);
        RB.linearVelocity = velocity;
    }

    void PlayerJump(float JumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Debug.Log("Jump pressed");
            RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void MouseRotaion()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        cameraVerticalAngle -= mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -maxLookAngle, maxLookAngle);
        playerCamera.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);
    }

    void CameraBob()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        
        bobTimer += Time.deltaTime * bobFrequency;
        float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
        playerCamera.localPosition = defaultCameraLocalPos + new Vector3(0, bobOffset, 0);
        
    }
}
