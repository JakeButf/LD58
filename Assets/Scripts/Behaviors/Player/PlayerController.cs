using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    bool SHOW_DEBUG = true;
    [SerializeField] private CharacterController playerController;

    [Header("Look Settings")]
    [SerializeField] private Camera camera;
    private float pitch = 0f;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    void Start()
    {
        if(playerController == null) playerController = GetComponent<CharacterController>();
        if(camera == null) camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug Info
        if (SHOW_DEBUG)
        {
            Debug.Log($"Player Position: {transform.position}");
            Debug.Log($"Camera Position: {camera.transform.position}");
            Debug.Log($"Player Rotation: {transform.rotation.eulerAngles}");
            Debug.Log($"Camera Rotation: {camera.transform.rotation.eulerAngles}");
        }

        //Camera
        float mouseX = Input.GetAxis("Mouse X") * PlayerInput.MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * PlayerInput.MouseSensitivity; 

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerController.transform.Rotate(Vector3.up * mouseX);
        //Movement
        float speed = moveSpeed;
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(PlayerInput.MoveUp)) velocity += transform.forward;
        if (Input.GetKey(PlayerInput.MoveDown)) velocity -= transform.forward;
        if (Input.GetKey(PlayerInput.MoveLeft)) velocity -= transform.right;
        if (Input.GetKey(PlayerInput.MoveRight)) velocity += transform.right;

        if (Input.G)

        playerController.Move(velocity.normalized * speed * Time.deltaTime);

        if(playerController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }
}
