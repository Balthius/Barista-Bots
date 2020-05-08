using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvasController : MonoBehaviour
{
    public ScoreManager scorePanel;

    private void OnEnable()
    {
        SetPanelInactive();
    }
    public void SetPanelInactive()
    {
        scorePanel.gameObject.SetActive(false);
    }

    public void SetPanelActive()
    {

        scorePanel.gameObject.SetActive(true);
    }

    public void PushScore(int finalScore)
    {
        scorePanel.ChooseSprite(finalScore);
    }
}
