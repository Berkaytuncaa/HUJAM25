using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public bool isBlack;
    public bool isWhite;
    public bool isGlove;
    public bool isCap;
    
    private void Awake()
    {
        // --- Singleton logic ---
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // persist between scenes
    }
}
