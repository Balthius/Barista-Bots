using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : MonoBehaviour {

    [SerializeField]
    private GameObject itemLocation; 

	public void GoToObject()
    {
        Camera mainCamera = FindObjectOfType<Camera>();

        mainCamera.ScreenToWorldPoint(itemLocation.transform.position);
    }
}
