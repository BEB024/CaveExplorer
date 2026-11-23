using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelExit : MonoBehaviour
{
    // Determines what happens when entering the portal.
    public enum ExitType
    {
        GoToLevel,
        ResetToLevelOne
    }

    [Header("Portal Settings")]
    [SerializeField] private ExitType exitType = ExitType.GoToLevel;

    // Drag and drop the destination scene in the Inspector.
    // SceneAsset is only available in the Unity Editor.
#if UNITY_EDITOR
    [SerializeField] private SceneAsset destinationScene;
#endif

    // Stores the scene path for use in built games.
    [SerializeField, HideInInspector]
    private string destinationScenePath;

    // Scene to load when resetting the game.
    [SerializeField] private string firstLevelSceneName = "Level 1";

    [Header("Transition Settings")]
    [SerializeField] private float levelLoadDelay = 1f;

    // Prevents the portal from activating multiple times.
    private bool isLoading = false;

#if UNITY_EDITOR
private void OnValidate()
{
    if (destinationScene != null)
    {
        destinationScenePath = AssetDatabase.GetAssetPath(destinationScene);
    }
    else
    {
        destinationScenePath = "";
    }
}
#endif

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only allow the player to activate the portal.
        if (!other.CompareTag("Player") || isLoading)
        {
            return;
        }

        // Choose the correct destination based on exit type.
        string targetScene = exitType == ExitType.ResetToLevelOne
            ? firstLevelSceneName
            : destinationScenePath;

        // Prevent loading an unassigned scene.
        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogError("No destination scene assigned!", this);
            return;
        }

        // Ensure the scene is included in the Build Profile.
        if (!Application.CanStreamedLevelBeLoaded(targetScene))
        {
            Debug.LogError(
                $"Scene '{targetScene}' is not in the active Build Profile!",
                this
            );
            return;
        }

        isLoading = true;
        StartCoroutine(LoadDestination(targetScene));
    }

    IEnumerator LoadDestination(string targetScene)
    {
        // Wait before transitioning to the destination.
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        // Reset persistent scene objects before loading.
        if (ScenePersist.Instance != null)
        {
            ScenePersist.Instance.ResetScenePersist();
        }

        // Allow Unity to process destruction.
        yield return null;

        // Load the assigned destination scene.
        SceneManager.LoadScene(targetScene);
    }
}

// Level mames instead of SceneAsset references can be used for simplicity, but using SceneAsset allows for better editor integration and validation.

/*

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // Determines what happens when the player enters this portal.
    public enum ExitType
    {
        GoToLevel,
        ResetToLevelOne
    }

    [Header("Portal Settings")]
    [SerializeField] private ExitType exitType = ExitType.GoToLevel;

    // Scene to load when this portal leads to another level.
    [SerializeField] private string destinationSceneName;

    // The first level of the game.
    [SerializeField] private string firstLevelSceneName = "Level 1";

    [Header("Transition Settings")]
    [SerializeField] private float levelLoadDelay = 1f;

    // Prevents the portal from triggering multiple times.
    private bool isLoading = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only the player can activate the portal.
        if (!other.CompareTag("Player") || isLoading)
        {
            return;
        }

        // Determine the destination based on the portal's exit type.
        string targetScene = exitType == ExitType.ResetToLevelOne
            ? firstLevelSceneName
            : destinationSceneName;

        // Make sure the scene name is valid.
        if (string.IsNullOrWhiteSpace(targetScene))
        {
            return;
        }

        // Verify that the scene is included in the Build Profile.
        if (!Application.CanStreamedLevelBeLoaded(targetScene))
        {
            return;
        }

        isLoading = true;

        // Begin the scene transition.
        StartCoroutine(LoadDestination(targetScene));
    }

    IEnumerator LoadDestination(string targetScene)
    {
        // Wait before transitioning to the destination.
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        // Reset scene-persistent objects before loading.
        if (ScenePersist.Instance != null)
        {
            ScenePersist.Instance.ResetScenePersist();
        }

        // Allow Unity to process object destruction.
        yield return null;

        // Load the scene assigned to this specific portal.
        SceneManager.LoadScene(targetScene);
    }
}

*/