using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // Number of lives the player has
    [SerializeField] int totalScore = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

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

    void Start()
    {
        livesText.text = playerLives.ToString() + "L";
        scoreText.text = totalScore.ToString() + "S";
    }

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

    public void AddToScore(int pointsToAdd)
    {
        totalScore += pointsToAdd; // Add the points to the total score
        scoreText.text = totalScore.ToString() + "S"; // Update the score text
    }

    IEnumerator TakeLife()
    { // When player dies, reduce the number of lives by 1 and reload the current scene
        playerLives--; // Reduce the number of lives by 1

        yield return new WaitForSeconds(levelLoadDelay); // Pause here for 1 second

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index
        SceneManager.LoadScene(currentSceneIndex); // Reload the current scene
        livesText.text = playerLives.ToString() + "L";
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSeconds(levelLoadDelay); // Pause here for 1 second

        if (ScenePersist.Instance != null) // Reset persistent coins and enemies
        {
            ScenePersist.Instance.ResetScenePersist();
        }

        yield return null; // Allow Unity to destroy ScenePersist
        
        SceneManager.LoadScene(0); // Restart the game from the first scene
        
        Destroy(gameObject); // Destroy the old GameSession after initiating the reload
    }


}
