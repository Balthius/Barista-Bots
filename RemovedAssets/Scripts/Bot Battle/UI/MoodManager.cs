using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] moodArray;

    public void UpdateMood(int current)
    {
        foreach(GameObject panel in moodArray)
        {
            panel.gameObject.SetActive(false);

        }
        Debug.Log("Current " + current);
        moodArray[current].gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
