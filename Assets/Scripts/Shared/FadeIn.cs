using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float fadeInTime = 2;

	private Image fadePanel;
	private Color currentColor = Color.black;
	// Use this for initialization

	void Start () 
	{
		fadePanel = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Time.timeSinceLevelLoad < fadeInTime) 
		{
			// delta time is the 'length of the frame' and fade in time
			//is the duration of fade in. so if the frame length is 2 seconds and the fade in time is 2 seconds the fade will be 100%
			float alphaChange = Time.deltaTime/ fadeInTime;
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
		} 
		else
		{
			gameObject.SetActive(false);
		}
	}


}
