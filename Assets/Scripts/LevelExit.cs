using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (other.CompareTag("Player"))
        {
            // Load the next level
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
