using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public static float timer = 300f;
    public static bool timeStarted = true;

    void Update()
    {
        if (timeStarted == true)
        {
            timer -= Time.deltaTime;
        }
    }

    void OnGUI()
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;

        //GUI.Label(new Rect(10, 10, 250, 100), minutes + ":" + Mathf.RoundToInt(seconds));
    }
}
