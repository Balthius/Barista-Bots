using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] int scoreVal;

    
    [SerializeField] private Color hitColor;

    private Vector3 targetLocation;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, 3f);

        if(transform.position == targetLocation)
        {
            Destroy(this.gameObject);
        }
        
    }
    public void SetTargetLocation(Vector3 target)
    {
        targetLocation = target;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<BattleGameManager>().AdjustTime(scoreVal, hitColor);
        Destroy(this.gameObject);
    }
 
}
