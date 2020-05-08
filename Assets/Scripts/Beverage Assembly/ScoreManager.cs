using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private Image overlayImage;
    [SerializeField] private ScoreHandler scoreHandler = null;


    private void Start()
    {
        overlayImage.gameObject.SetActive(false);
    }
    public void ChooseSprite(int score)
    {
        Debug.Log("Set overlay active");
        overlayImage.gameObject.SetActive(true);
        Debug.Log("Score" + score);

        overlayImage.sprite = scoreSprites[score - 1];

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        scoreHandler.IncreaseCurrency(score);
    }
}
