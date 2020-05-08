using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Game : MonoBehaviour
{
    public GameObject loadingScreen;
    private SpriteRenderer sprRenderer;
    
    void Start()
    {
        sprRenderer = loadingScreen.GetComponent<SpriteRenderer>();
    }

    public void ReturnToCafe()
    {
        sprRenderer.enabled = true;
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Bootup_Screen");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
