using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance {
        get {
            return _instance;
        }
    }

    private CafeSpot[] _spots;

    void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _spots = FindObjectsOfType(typeof(CafeSpot)) as CafeSpot[];
    }

    void Start()
    {
        foreach (var spot in _spots) {
            spot.Initialize();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Cafe")) {
            if (PlayerPrefs.HasKey("cafeScore")) {
                var score = PlayerPrefs.GetInt("cafeScore");
                foreach (var spot in _spots) {
                    spot.UpdateStateFromMiniGame(score);
                }
                PlayerPrefs.DeleteKey("cafeScore");
            }
        }
    }
}
