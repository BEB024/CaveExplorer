using UnityEngine;

public class GameSession : MonoBehaviour
{
    void Awake()
    {
        // Create a singleton pattern to ensure only one instance of GameSession exists
        int numberGameSessions = FindObjectsOfType<GameSession>(FindObjectsSortMode.None).Length;
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
