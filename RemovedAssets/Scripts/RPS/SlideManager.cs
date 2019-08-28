using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    [SerializeField] GameObject emojiManager;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField] private bool detectSwipeOnlyAfterRelease = true;

    [SerializeField] private float minDistanceForSwipe = 30f;


    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }
            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;//changed to up
                DetectSwipe();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;//Changed to up
                DetectSwipe();
            }
        }

        //if (Input.GetButtonDown("Fire1"))
        //{

        //    fingerDownPosition = Input.mousePosition;
        //}
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    fingerDownPosition = Input.mousePosition;
        //    DetectSwipe();
        //}
    }


    private void DetectSwipe()
    {
        if(SwipeDistanceCheckMet())
        {
            if(IsHorizontalSwipe())
            {
               if((fingerDownPosition.x - fingerUpPosition.x) > 0)
                {
                    emojiManager.GetComponent<Emoji_Manager>().NumberUp();
                }
               else
                {

                    emojiManager.GetComponent<Emoji_Manager>().NumberDown();
                }
                fingerUpPosition = fingerDownPosition;
            }
        }
        else
        {
            Debug.Log("SelectEmoji");
            emojiManager.GetComponent<Emoji_Manager>().SelectEmoji();
            
        }
    }

    private bool IsHorizontalSwipe()
    {
        return HorizontalMovementDistance() > VerticalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

}

