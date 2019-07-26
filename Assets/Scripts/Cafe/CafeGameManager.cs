using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeGameManager: MonoBehaviour {

    
    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject world;
    public GameObject loadingScreen;
    public static bool canMove;
    private bool notPortrait = true;

    //[SerializeField] private AudioClip bgmClip;
    // Use this for initialization
    void Start () {
        //AudioManager.instance.StartBGM(bgmClip);
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.deviceOrientation == DeviceOrientation.Portrait && notPortrait)
        {
            notPortrait = false;
            loadingScreen.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(LoadTitle());
        }
    }

    IEnumerator LoadTitle()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Assets/Scenes/BootUp_Screen.unity", LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private void OnMouseDown()
    {
        if (canMove)
        {
            screenPoint = Camera.main.WorldToScreenPoint(world.transform.position);
            offset = world.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        
    }
    private void OnMouseDrag()
    {
        if (canMove)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            world.transform.position = new Vector3(Mathf.Clamp(curPosition.x, -48.5f, -11.5f), Mathf.Clamp(curPosition.y, -40.4f, -19.6f), curPosition.z);
            //world.transform.position = new Vector3(Mathf.Clamp(curPosition.x, -44.5f, -19.6f), Mathf.Clamp(curPosition.y, -15.5f, -40.4f), curPosition.z);
            //transform.position = curPosition;
        }

    }
}
