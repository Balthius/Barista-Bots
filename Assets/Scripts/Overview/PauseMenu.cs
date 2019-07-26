using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject menuButtons;


    public static PauseMenu instance = null;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        menuButtons.gameObject.SetActive(false);
    }
    void Start ()
    {
        pauseMenu.gameObject.SetActive(false);
        menuButtons.gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        pauseButton.gameObject.SetActive(false);
        menuButtons.gameObject.SetActive(true);
        AudioManager.instance.LowerBGMVolume();

        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        menuButtons.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        AudioManager.instance.ResetBGMVolume();

        Time.timeScale = 1;
    }
}
