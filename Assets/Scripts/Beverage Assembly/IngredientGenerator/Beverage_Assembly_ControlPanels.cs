using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beverage_Assembly_ControlPanels : MonoBehaviour
{
    [SerializeField]
    GameObject beltObj;

    [SerializeField]AudioClip bgAudio;
    [SerializeField]GameObject audioManager;
    
    public int currentTime;
    
    public float spawnTime = 10f;
    public float gameSpeed = 10f;

    private void Start()
    {
        audioManager.GetComponent<AudioManager>().StartBGM(bgAudio);

    }
    public void SpawnNewOrder()
    { 
            Belt_Controller belt = beltObj.GetComponent<Belt_Controller>();
            belt.MoveOrders();  
    }
}
   