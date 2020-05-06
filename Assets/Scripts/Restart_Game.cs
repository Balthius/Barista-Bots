using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Game : MonoBehaviour {

    public GameObject loadingScreen;
    private SpriteRenderer sprRenderer;
    // Use this for initialization
    void Start () {
        sprRenderer = loadingScreen.GetComponent<SpriteRenderer>();
    }
    public void ReturnToCafe()
    {
        sprRenderer.enabled = true;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        Debug.Log("returning to cafe");
        yield return new WaitForSeconds(2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Assets/Scenes/Bootup_Screen.unity");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
