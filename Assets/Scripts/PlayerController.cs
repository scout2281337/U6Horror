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

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        PlayerMovement(speed);
        PlayerJump(jumpForce);
        MouseRotaion();
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
        float mouseX  = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        cameraVerticalAngle -= mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -maxLookAngle, maxLookAngle);
        playerCamera.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

    }
}
