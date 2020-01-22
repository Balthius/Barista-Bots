using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleGameManager : MonoBehaviour
{
    public TMP_Text scoreText, timeText;
    public GameObject bubbleManager, scorePanel;

    private bool gameEnded = false;
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
        score = Mathf.Clamp(score, 0, 100);
        currentTime -= Time.deltaTime;
        scoreText.text = score.ToString();
        timeText.text = currentTime.ToString("0.00");

        if(currentTime <= 0 && !gameEnded)
        {
            gameEnded = true;
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
        score += Mathf.Clamp((activeManagers / 3), 1, 10);
        if(time <= 0)
        {
        StartCoroutine(UpDifficulty());
        }
    }
    private void GameOver()
    {
        scorePanel.SetActive(true);
        int finalscore = score/20;

        if(finalscore <= 0)
        {
            score = 0;
        }
        
        if(finalscore > 5)
        {
        finalscore = 5;
        }
        this.GetComponent<PushScore>().CommitScore(score);
        scorePanel.GetComponent<ScoreManager>().ChooseSprite(finalscore);
    }
}
