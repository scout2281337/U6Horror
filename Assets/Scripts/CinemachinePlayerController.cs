using UnityEngine;
using Unity.Cinemachine;

public class CinemachinePlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 350f;

    [SerializeField] private CinemachinePanTilt panTilt; // ���� ������������ ��������� PanTilt � ������
    [Header("����������� ������ // Camera perlin")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin channelPerlin;
    
    private float _AmplitudeGain;
    private float _FrequencyGain;

    private Rigidbody RB;
    private bool canJump = false;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkRadius = 0.3f;
    [SerializeField] private Transform groundCheck;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SyncRotationWithCamera();
        PlayerMovement();
        PlayerJump();
        canJump = Physics.Raycast(groundCheck.position, Vector3.down, checkRadius, groundMask);
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        Vector3 velocity = new Vector3(moveDirection.x * speed, RB.linearVelocity.y, moveDirection.z * speed);
        RB.linearVelocity = velocity;
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void SyncRotationWithCamera()
    {
        if (panTilt != null)
        {
            // ������� ������ �� Y, ����� ���� �������������� � �������
            transform.rotation = Quaternion.Euler(0f, panTilt.PanAxis.Value, 0f);
        }
    }
}
