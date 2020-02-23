using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScore : MonoBehaviour
{

    public void CommitScore(int score)
    {
        int curScore;

        if(PlayerPrefs.HasKey("cafeScore"))
        {

            curScore = PlayerPrefs.GetInt("cafeScore");
            PlayerPrefs.SetInt("cafeScore", (curScore + score));
        }
        else
        {
        PlayerPrefs.SetInt("cafeScore", score);
        }
    }
}
