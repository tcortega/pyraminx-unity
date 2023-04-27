using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 6.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 6.0f;

    [SerializeField]
    private float zoomSpeed = 2.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(-40, 40);

    private void Start()
    {
        InitializeCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Rotate the camera when left mouse button is pressed
            RotateCamera();
        }

        if (Input.GetMouseButton(1))
        {
            // Zoom the camera when right mouse button is pressed
            ZoomCamera();
        }
    }

    private void InitializeCameraPosition()
    {
        currentRotation = new Vector3(rotationX, rotationY);
        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distanceFromTarget;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        // Apply clamping for x rotation 
        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        // Apply damping between rotation changes
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = target.position - transform.forward * distanceFromTarget;
    }

    private void ZoomCamera()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        distanceFromTarget -= scrollWheel * zoomSpeed;
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, 2.0f, 15.0f);

        // Update the camera position based on the new distance
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
