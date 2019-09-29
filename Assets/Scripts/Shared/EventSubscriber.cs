using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventSubscriber : MonoBehaviour
{
    public EventSubscription subscription;
    public event Action<string, object[]> OnReact;

    void OnEnable()
    {
        subscription.Subscribe(this);
    }
    
    void OnDisable()
    {
        subscription.Unsubscribe(this);
    }

    public void React(string subscriptionEvent, params object[] parameters)
    {
        OnReact(subscriptionEvent, parameters);
    }
}