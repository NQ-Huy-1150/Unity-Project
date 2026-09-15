using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Spawn")]
    public Transform spawnPoint; // Kéo GameObject SpawnPoint vào đây

    [Header("Tốc độ di chuyển")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float slowSpeed = 2.5f;

    [Header("Nhảy & trọng lực")]
    public float jumpForce = 8f;
    public float gravity = -20f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (spawnPoint != null)
        {
            controller.enabled = false;
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            controller.enabled = true;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Tốc độ theo Shift
        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else if (Input.GetKey(KeyCode.RightShift))
            speed = slowSpeed;

        // Di chuyển: A/D strafe ngang, W/S tiến lùi
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);


    }
}