using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         // Movement speed
    private int keysCollected = 0;       // Number of keys collected
    private int maxHealth = 2;           // Max health of the player
    private int currentHealth;           // Current health of the player

    public Transform cameraTransform;    // Reference to the camera's transform

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    private void Update()
    {
        // Basic movement input for a 3D game (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Get the camera's forward and right directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Make sure the movement is aligned to the ground (no movement on the y-axis)
        forward.y = 0f;
        right.y = 0f;

        // Normalize to remove scaling
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction relative to the camera
        Vector3 direction = (forward * vertical + right * horizontal).normalized;

        // Apply movement
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Optional: Handle Jumping, if needed (basic example below)
        // If you'd like to add jumping, you can expand the script further with physics-based movement.
    }

    public void CollectKey()
    {
        keysCollected++;
        Debug.Log($"Keys collected: {keysCollected}");
    }

    public int GetKeyCount()
    {
        return keysCollected;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took damage! Health remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died. Game Over.");
        GameManager.Instance.GameOver();  // Call the GameOver method in the GameManager
    }
}
