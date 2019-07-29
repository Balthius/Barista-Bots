using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomerServiceCombat
{
    public class ComputerManager : MonoBehaviour
    {
        public EventSubscription eventSubscription;
        public Animator animator;

        EventSubscriber eventSubscriber;
        SwipeDetectionOnUIElement swipeDetector;
        float swipeThreshold;
        int emotionState = 0;

        void Awake()
        {
            eventSubscriber = GetComponent<EventSubscriber>();
            swipeDetector = GetComponent<SwipeDetectionOnUIElement>();
            swipeThreshold = Screen.width * 0.1f;
        }

        void OnEnable()
        {
            eventSubscriber.OnReact += React;
            swipeDetector.OnSwipeDetected += OnSwipeDetected;
        }

        void OnDisable()
        {
            eventSubscriber.OnReact -= React;
            swipeDetector.OnSwipeDetected -= OnSwipeDetected;
        }

        void React(string eventName, object[] parameters)
        {
            switch (eventName)
            {
                case "FIGHT":
                    eventSubscription.Notify("EMOTION_SELECTED", emotionState);
                    break;
            }
        }

        void OnSwipeDetected (float deltaX, float deltaY)
        {
            if (Mathf.Abs(deltaX) < swipeThreshold || deltaX == 0)
            {
                return;
            }

            emotionState = ((deltaX > 0 ? ++emotionState : --emotionState) + 3) % 3;
            animator.SetInteger("emotionState", emotionState);
        }
    }
}