using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootUp_Fades : MonoBehaviour {

    private SpriteRenderer[] frames;
    private int onFrame = 0;
    private Color tmpColor;
    private bool fadedOut = false;
    private bool fadedIn = false;
	// Use this for initialization
	void Start () {
        frames = this.GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(fadeOut(frames[0]));
	}

    IEnumerator fadeOut(SpriteRenderer spr)
    {
        yield return new WaitForSeconds(0.02f);
        if (spr.color.a != 0)
        {
            tmpColor = spr.color;
            tmpColor.a -= 0.01f;
            spr.color = tmpColor;
        }
        else
        {
            fadedOut = true;
            onFrame++;
            if (onFrame == 4)
            {
                onFrame = 0;
            }
        }
    }

    IEnumerator fadeIn(SpriteRenderer spr)
    {
        yield return new WaitForSeconds(0.02f);
        if (spr.color.a != 0)
        {
            tmpColor = spr.color;
            tmpColor.a += 0.01f;
            spr.color = tmpColor;
        }
        else
        {
            fadedIn = true;
            onFrame++;
            if (onFrame == 4)
            {
                onFrame = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadedIn)
        {
            fadedIn = false;
            if (onFrame == 0)
            {
                StopCoroutine(fadeIn(frames[3]));
                StartCoroutine(fadeOut(frames[3]));
            }
            else
            {
                StopCoroutine(fadeIn(frames[onFrame-1]));
                StartCoroutine(fadeOut(frames[onFrame-1]));
            }   
        }

        if (fadedOut)
        {
            fadedOut = false;
            if (onFrame == 0)
            {
                StopCoroutine(fadeOut(frames[3]));
                StartCoroutine(fadeIn(frames[0]));
            }
            else
            {
                StopCoroutine(fadeOut(frames[onFrame - 1]));
                StartCoroutine(fadeIn(frames[onFrame]));
            }
        }
    }
}
