using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RunGame : MonoBehaviour
{
    public string targetScene;


    public void LoadLevel()
    {
        SceneManager.LoadScene(targetScene);
    }


}
