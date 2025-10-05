using UnityEngine;

public class PlayerIntroController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private bool invertY = false;

    private float yaw;
    private float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        sensitivity = PlayerInput.MouseSensitivity;
        yaw = angles.y;
        pitch = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * (invertY ? 1f : -1f);

        yaw += mouseX;
        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}


