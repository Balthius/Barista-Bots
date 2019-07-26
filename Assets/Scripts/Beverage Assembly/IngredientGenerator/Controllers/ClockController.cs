using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour {
    
    [SerializeField] private float gameLength;
    [SerializeField] private Text timerText;
    private float currentTime;

    private void Start()
    {
        currentTime = gameLength;
    }
    private void Update()
    {
        Debug.Log(currentTime);
         currentTime -= Time.deltaTime;
        SetTimer(currentTime);
        if(currentTime <= 0)
        {
            Belt_Controller beltObj = GameObject.FindGameObjectWithTag("Belt").GetComponent<Belt_Controller>();
            beltObj.GetComponent<Belt_Controller>().CheckGameStatus();
        }
    }
    
    private void SetTimer( float time)
    {
        if (time >= 0)
        {
            timerText.text = time.ToString("F1");
        }
    }
    

}
