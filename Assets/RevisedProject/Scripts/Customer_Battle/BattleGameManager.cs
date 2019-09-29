using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleGameManager : MonoBehaviour
{
    public TMP_Text scoreText, timeText;
    public GameObject bubbleManager, scorePanel;

    static int score = 0;
    public int startTime;
    private float currentTime;

    private int activeManagers;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        
        StartCoroutine(UpDifficulty());
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        scoreText.text = score.ToString();
        timeText.text = currentTime.ToString("0.00");

        if(currentTime <= 0)
        {
            GameOver();
        }
    }

    // Update is called once per frame
    private IEnumerator UpDifficulty()
    {
        if(activeManagers < 10)
        {
            activeManagers++;
            GameObject newBubbleManager = Instantiate(bubbleManager);
            newBubbleManager.transform.parent = transform;
            yield return new WaitForSeconds(5);
            StartCoroutine(UpDifficulty());
        }
    }

    public void AdjustTime(int time, Color hitColor)
    {
        scoreText.GetComponent<TextColorController>().SetColor(hitColor);
        
        timeText.GetComponent<TextColorController>().SetColor(hitColor);
        currentTime += time;
        score += (activeManagers/3) * time;
        if(time <= 0)
        {
        StartCoroutine(UpDifficulty());
        }
    }
    private void GameOver()
    {
        int finalscore = score/40;

        if(finalscore <= 0)
        {
            score = 0;
        }
        
        if(finalscore > 5)
        {
        finalscore = 5;
        }
        scorePanel.GetComponent<ScoreManager>().ChooseSprite(finalscore);
    }
}
