using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScenePersist()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
