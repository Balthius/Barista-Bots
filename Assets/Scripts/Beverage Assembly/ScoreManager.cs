using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private GameObject overlayImage;


    private void Start()
    {
        overlayImage.SetActive(false);
    }
    public void ChooseSprite(int score)
    {
        Debug.Log("Set overlay active");
        overlayImage.SetActive(true);
        Debug.Log("Score" + score);
        overlayImage.GetComponent<SpriteRenderer>().sprite = scoreSprites[Mathf.Clamp(score,1, 5)-1];
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        this.GetComponent<PushScore>().CommitScore(score);
    }
}
