using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;                // Reference to the player character
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset relative to the player
    public float followSpeed = 10f;         // Smoothness of camera follow
    public float rotateSpeed = 100f;        // Speed of camera rotation

    public float minVerticalAngle = -20f;   // Minimum vertical angle (look down)
    public float maxVerticalAngle = 60f;    // Maximum vertical angle (look up)

    private float currentYaw = 0f;          // Horizontal rotation
    private float currentPitch = 0f;        // Vertical rotation

    void LateUpdate()
    {
        if (player == null) return;

        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Update rotation angles
        currentYaw += mouseX * rotateSpeed * Time.deltaTime;
        currentPitch -= mouseY * rotateSpeed * Time.deltaTime;

        // Clamp the vertical angle
        currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);

        // Calculate the camera's position and rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the player
        transform.LookAt(player.position + Vector3.up * 2f); // Slightly above the player for better framing
    }
}
