using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private GameObject overlayImage;
    [SerializeField] private ScoreHandler scoreHandler = null;


    private void Start()
    {
        overlayImage.SetActive(false);
    }
    public void ChooseSprite(int score)
    {
        Debug.Log("Set overlay active");
        overlayImage.SetActive(true);
        Debug.Log("Score" + score);

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        scoreHandler.IncreaseCurrency(score);
    }
}
