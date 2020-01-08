using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootUp : MonoBehaviour {

    private SpriteRenderer sprRenderer;
    private bool notLandscape = true;
    public GameObject loadingScreen;
	// Use this for initialization
	void Start () {
        sprRenderer = loadingScreen.GetComponent<SpriteRenderer>();
	}

    private void OnMouseDown()
    {
        //SceneManager.LoadScene("Assets/Scenes/Intro.unity");
    }

    // Update is called once per frame
    void Update () {
        if (notLandscape && (Input.deviceOrientation == DeviceOrientation.LandscapeRight))
        {
            notLandscape = false;
            sprRenderer.enabled = true;
            StartCoroutine(LoadCafe());
        }
    }

    IEnumerator LoadCafe()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Assets/Scenes/Cafe.unity", LoadSceneMode.Single);
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
}
