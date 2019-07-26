using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void LoadLevel(string targetScene)
    {
        SceneManager.LoadScene(targetScene, LoadSceneMode.Single);

        Time.timeScale = 1;
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void ResetMinigame()
    {

        Time.timeScale = 1;
        FindObjectOfType<StatusController>().ResetGame();
    }


}

