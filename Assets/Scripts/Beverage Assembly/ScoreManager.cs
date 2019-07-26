using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private GameObject overlayImage;
    public void ChooseSprite(int score)
    {
        Debug.Log("Score" + score);
        overlayImage.GetComponent<SpriteRenderer>().sprite = scoreSprites[score - 1];
        //Push final score to Cafe
    }
}
