using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numberScenePersists = FindObjectsByType<ScenePersist>().Length; // FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length was replaced for better performance in Unity 2023.1 and later versions
        if (numberScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
