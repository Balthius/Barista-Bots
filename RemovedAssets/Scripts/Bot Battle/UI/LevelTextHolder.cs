using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextHolder : MonoBehaviour
{//Attached To Response Text
    public int TextValue{ get; set; }
    private Text textDisplay;


    private void Start()
    {
        textDisplay = this.gameObject.GetComponent<Text>();
    }
    public void ReceiveValue(int num)
    {
        TextValue = num;
    }

    private void Update()
    {
        textDisplay.text = TextValue.ToString();
    }

}
