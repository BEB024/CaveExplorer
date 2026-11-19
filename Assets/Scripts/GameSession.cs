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



//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameSession : MonoBehaviour
//{
//    // Static singleton instance.
//    // Allows other scripts to access the current GameSession.
//    public static GameSession Instance { get; private set; }

//    [Header("Game Settings")]
//    [SerializeField] int playerLives = 3;
//    [SerializeField] int totalScore = 0;

//    [Header("UI References")]
//    [SerializeField] TextMeshProUGUI livesText;
//    [SerializeField] TextMeshProUGUI scoreText;

//    [Header("Level Settings")]
//    [SerializeField] float levelLoadDelay = 1f;

//    // Prevent multiple death coroutines from running at once.
//    private bool isProcessingDeath = false;

//    void Awake()
//    {
//        // Destroy duplicate GameSession objects.
//        if (Instance != null && Instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // Register this GameSession as the singleton.
//        Instance = this;

//        // Preserve GameSession between scene loads.
//        DontDestroyOnLoad(gameObject);
//    }

//    void OnEnable()
//    {
//        // Listen for scene loading events.
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnDisable()
//    {
//        // Unsubscribe to avoid unnecessary callbacks.
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//    }

//    void Start()
//    {
//        UpdateUI();
//    }

//    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        // Find the UI elements in the newly loaded scene.
//        // This is necessary if the UI objects were destroyed
//        // during the previous scene reload.
//        if (livesText == null)
//        {
//            GameObject livesObject =
//                GameObject.Find("Lives Text");

//            if (livesObject != null)
//            {
//                livesText =
//                    livesObject.GetComponent<TextMeshProUGUI>();
//            }
//        }

//        if (scoreText == null)
//        {
//            GameObject scoreObject =
//                GameObject.Find("Score Text");

//            if (scoreObject != null)
//            {
//                scoreText =
//                    scoreObject.GetComponent<TextMeshProUGUI>();
//            }
//        }

//        // Refresh the UI after loading a scene.
//        UpdateUI();
//    }

//    void UpdateUI()
//    {
//        // Update lives if the text reference exists.
//        if (livesText != null)
//        {
//            livesText.text = playerLives.ToString() + "L";
//        }

//        // Update score if the text reference exists.
//        if (scoreText != null)
//        {
//            scoreText.text = totalScore.ToString() + "S";
//        }
//    }

//    public void AddToScore(int pointsToAdd)
//    {
//        // Increase the player's score.
//        totalScore += pointsToAdd;

//        // Refresh the score display.
//        UpdateUI();
//    }

//    public void ProcessPlayerDeath()
//    {
//        // Ignore additional death events while processing.
//        if (isProcessingDeath)
//        {
//            return;
//        }

//        isProcessingDeath = true;

//        // Check whether the player has lives remaining.
//        if (playerLives > 1)
//        {
//            StartCoroutine(TakeLife());
//        }
//        else
//        {
//            StartCoroutine(ResetGameSession());
//        }
//    }

//    IEnumerator TakeLife()
//    {
//        // Remove one life.
//        playerLives--;

//        // Wait before reloading the current level.
//        yield return new WaitForSeconds(levelLoadDelay);

//        int currentSceneIndex =
//            SceneManager.GetActiveScene().buildIndex;

//        // Reload the current level while preserving
//        // GameSession and ScenePersist.
//        SceneManager.LoadScene(currentSceneIndex);

//        // Refresh the lives and score display.
//        UpdateUI();

//        // Allow future death events.
//        isProcessingDeath = false;
//    }

//    IEnumerator ResetGameSession()
//    {
//        // Wait before restarting the game.
//        yield return new WaitForSeconds(levelLoadDelay);

//        // Remove persistent coins and enemies.
//        if (ScenePersist.Instance != null)
//        {
//            ScenePersist.Instance.ResetScenePersist();
//        }

//        // Allow Unity to process the destruction.
//        yield return null;

//        // Reset the values before restarting.
//        playerLives = 3;
//        totalScore = 0;

//        // Reload the first scene.
//        SceneManager.LoadScene(0);

//        // Refresh the UI with the initial values.
//        UpdateUI();

//        // Allow death processing again.
//        isProcessingDeath = false;
//    }

//    void OnDestroy()
//    {
//        // Clear the singleton when the GameSession
//        // instance is destroyed.
//        if (Instance == this)
//        {
//            Instance = null;
//        }
//    }
//}
