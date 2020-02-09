using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColorController : MonoBehaviour
{
    Color lerpedColor;
    TextMeshPro currentText;
    public void SetColor(Color startColor)
    {
        //currentText = GetComponent<TextMeshPro>();
        //StartCoroutine(ColorLerp(startColor));
    }
    private IEnumerator ColorLerp(Color start)
    {
        for(float t = 0.01f; t < 1; t+= 0.1f)
        {
            currentText.color = Color.Lerp(start,Color.black, 1);
            yield return null;
        }
    }
}
