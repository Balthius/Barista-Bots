using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UpdateCafeFromMiniGameScore (int score)
    {
        foreach (var spot in _spots) {
            spot.UpdateStateFromMiniGame(score);
        }
    }
}
