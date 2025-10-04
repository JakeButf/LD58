using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    bool SHOW_DEBUG = true;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Transform armTransform;

    [Header("Look Settings")]
    [SerializeField] private Camera camera;
    private float pitch = 0f;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Head Bob")]
    [SerializeField] private float bobFrequency = 1.5f;
    [SerializeField] private float bobHeight = 0.05f;
    [SerializeField] private float bobSmoothing = 8f;
    private float bobTimer = 0f;
    private Vector3 initialCameraPosition;
    private Vector3 velocity;

    void Start()
    {
        if (playerController == null) playerController = GetComponent<CharacterController>();
        if (camera == null) camera = Camera.main;
        initialCameraPosition = camera.transform.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug Info
        if (SHOW_DEBUG)
        {
            // Debug.Log($"Player Position: {transform.position}");
            // Debug.Log($"Camera Position: {camera.transform.position}");
            // Debug.Log($"Player Rotation: {transform.rotation.eulerAngles}");
            // Debug.Log($"Camera Rotation: {camera.transform.rotation.eulerAngles}");
            // Debug.Log($"Grounded? {playerController.isGrounded}");
        }

        //Camera
        float mouseX = Input.GetAxis("Mouse X") * PlayerInput.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * PlayerInput.MouseSensitivity;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        armTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerController.transform.Rotate(Vector3.up * mouseX);
        //Movement
        float speed = moveSpeed;
        Vector3 move = Vector3.zero;
        if (Input.GetKey(PlayerInput.MoveUp)) move += transform.forward;
        if (Input.GetKey(PlayerInput.MoveDown)) move -= transform.forward;
        if (Input.GetKey(PlayerInput.MoveLeft)) move -= transform.right;
        if (Input.GetKey(PlayerInput.MoveRight)) move += transform.right;
        move = move.normalized * speed;
        playerController.Move(move * Time.deltaTime);

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (IsGrounded() && Input.GetKeyDown(PlayerInput.Jump))
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
        }

        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
        Headbob(move);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerController.height / 2f + 0.1f);
    }

    void Headbob(Vector3 move)
    {
        bool isMoving = move.magnitude > 0.1f && IsGrounded();
        Vector3 targetPosition = initialCameraPosition;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float offsetY = Mathf.Sin(bobTimer * 2f) * bobHeight;
            float offsetX = Mathf.Cos(bobTimer) * bobHeight * 0.5f;
            targetPosition += new Vector3(offsetX, offsetY, 0f);
        }
        else
        {
            bobTimer = 0f;
        }
        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, targetPosition, Time.deltaTime * bobSmoothing);
    }
}
