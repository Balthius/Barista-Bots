using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_Run : MonoBehaviour {

    Animator anim;
    public GameObject loadingScreen;
    private SpriteRenderer sprRenderer;
    private bool animating;
    private int animNum;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        animating = false;
        animNum = 1;
        sprRenderer = loadingScreen.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (!animating)
        {
            anim.SetInteger("animation_num", animNum);
            animating = true;
            animNum++;
            StartCoroutine(waitTillEnd());
        }
        
    }

    IEnumerator waitTillEnd()
    {
        yield return new WaitForSeconds(1f);
        if (animNum == 20)
        {
            sprRenderer.enabled = true;
            StartCoroutine(LoadCafe());
        }
        animating = false;
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
    // Update is called once per frame
    void Update () {
		
	}
}
