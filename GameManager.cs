using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Restarting...");
        Invoke(nameof(RestartGame), 2f);
    }

    public void WinGame()
    {
        Debug.Log("Congratulations! You've unlocked the exit and won the game!");
        // Add any win effects or screens here
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the scene
    }
}
