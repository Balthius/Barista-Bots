using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioSource bgSource;

    public static AudioManager instance = null;

    [SerializeField] private float lowVolume = .75f;
    [SerializeField] private float highVolume = 1.00f;
	// Use this for initialization
	void Awake ()
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
	}
	
    public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }

    public void StartBGM(AudioClip clip)
    {
        bgSource.clip = clip;
        bgSource.Play();
    }
    
    public void LowerBGMVolume()
    {
        bgSource.volume = lowVolume;
    }


    public void ResetBGMVolume()
    {
        bgSource.volume = highVolume;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
