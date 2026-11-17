using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // Number of lives the player has
    [SerializeField] float levelLoadDelay = 1f; // Delay in seconds before reloading

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
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    IEnumerator TakeLife()
    {
        playerLives--; // Reduce the number of lives by 1

        yield return new WaitForSeconds(levelLoadDelay); // Pause here for 1 second

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index
        SceneManager.LoadScene(currentSceneIndex); // Reload the current scene
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSeconds(levelLoadDelay); // Pause here for 1 second

        SceneManager.LoadScene(0); // Load the first scene (index 0)
        Destroy(gameObject); // Destroy the GameSession object
    }

}
