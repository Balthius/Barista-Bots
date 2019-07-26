using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    GameObject belt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Belt_Controller beltCon = belt.GetComponent<Belt_Controller>();
        Destroy(collision.gameObject);
    }
}
