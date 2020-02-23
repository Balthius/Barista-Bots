using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Cafe_Bot : MonoBehaviour {

    public string name_of_mini_game;
    public GameObject loadingScreen;
    private SpriteRenderer sprRenderer;
    // Use this for initialization
    void Start () {
        sprRenderer = loadingScreen.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnTap()
    {
       sprRenderer.enabled = true;
       StartCoroutine(LoadMiniGame(name_of_mini_game));
    }

    IEnumerator LoadMiniGame(string name)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name_of_mini_game);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
