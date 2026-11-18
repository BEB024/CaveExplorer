using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f; // Delay before loading the next level

    private bool isLoading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || isLoading)
        {
            return;
        }

        isLoading = true;
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        // Wait for 1 second before loading the next level
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1; // Get the index of the next level

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) // If the next level index is equal to the total number of scenes in the build settings, it means we are at the last level
        {
            nextSceneIndex = 0; // Reset to the first level (index 0) if we are at the last level to prevent an error
        }

        if (ScenePersist.Instance != null)
        {
            ScenePersist.Instance.ResetScenePersist();
        }

        yield return null; // Allow Unity to process the destruction.

        SceneManager.LoadScene(nextSceneIndex); // Load the next level
    }
}