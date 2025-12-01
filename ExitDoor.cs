// ExitDoor.cs
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public int requiredKeys = 5;
    private bool isUnlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && !isUnlocked)
        {
            if (player.GetKeyCount() >= requiredKeys)
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("You need more keys to unlock the exit.");
            }
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("The exit door is now unlocked! You win!");
        GameManager.Instance.WinGame();
    }
}
