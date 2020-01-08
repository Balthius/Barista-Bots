using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public float speedOut;
    public float speedIn;
    private SpriteRenderer spriteColor;
    private Color tmpColor;
    public static bool dark;
	// Use this for initialization
	void Start () {
        spriteColor = this.GetComponent<SpriteRenderer>();
        dark = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (dark == true && spriteColor.color.a <= 0.8f)
        {
            tmpColor = spriteColor.color;
            tmpColor.a += 0.01f;
            spriteColor.color = tmpColor;
            // Debug.Log(spriteColor.color.a);
        }
        else if(spriteColor.color.a > 0)
        {
            tmpColor = spriteColor.color;
            tmpColor.a -= 0.01f;
            spriteColor.color = tmpColor;
            // Debug.Log(spriteColor.color.a);
        }
      
        
	}




}
