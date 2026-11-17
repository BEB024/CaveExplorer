using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // Number of lives the player has

    void Awake()
    {
        // Create a singleton pattern to ensure only one instance of GameSession exists
        int numberGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject); // Destroy the duplicate GameSession object, the player
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Leave the player as is
        }
    }

    // When player dies, reduce the number of lives by 1 and reload the current scene

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--; // Reduce the number of lives by 1
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index
        SceneManager.LoadScene(currentSceneIndex); // Reload the current scene
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0); // Load the first scene (index 0)
        Destroy(gameObject); // Destroy the GameSession object
    }

}
