using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{

    public float fadeOutTime = 10;

    SpriteRenderer fadeStructure;
    private Color currentColor = Color.black;
    // Use this for initialization

    void Start()
    {
         fadeStructure = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < fadeOutTime)
        {
            // delta time is the 'length of the frame' and fade in time
            //is the duration of fade in. so if the frame length is 2 seconds and the fade in time is 2 seconds the fade will be 100%
            float alphaChange = Time.deltaTime / fadeOutTime;
            currentColor.a -= alphaChange;
            fadeStructure.color = currentColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
