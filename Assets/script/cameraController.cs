using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;         // drag CameraRoot here
    public float followSpeed = 10f;

    [Header("Orbit")]
    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    [Header("Zoom")]
    public float distance = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float zoomSpeed = 2f;

    [Header("Collision")]
    public LayerMask collisionLayers;
    public float collisionRadius = 0.2f;

    private float _yaw;    // horizontal rotation
    private float _pitch;  // vertical rotation

    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start with current rotation
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleInput();
        HandleZoom();
        PositionCamera();
    }

    void HandleInput()
    {
        _yaw   += Input.GetAxis("Mouse X") * mouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _pitch  = Mathf.Clamp(_pitch, minVerticalAngle, maxVerticalAngle);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance  = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void PositionCamera()
    {
        // Desired rotation
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);

        // Desired position behind the target
        Vector3 desiredPos = target.position - rotation * Vector3.forward * distance;

        // Camera collision — push camera forward if something is in the way
        Vector3 direction = desiredPos - target.position;
        if (Physics.SphereCast(target.position, collisionRadius,
            direction.normalized, out RaycastHit hit,
            distance, collisionLayers))
        {
            desiredPos = target.position + direction.normalized * hit.distance;
        }

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position,
            desiredPos, followSpeed * Time.deltaTime);

        transform.LookAt(target.position);
    }
}