using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSubscription", menuName = "ScriptableObjects/EventSubscription", order = 1)]
public class EventSubscription : ScriptableObject
{
    List<EventSubscriber> subscribers = new List<EventSubscriber>(10);

    public void Subscribe(EventSubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void Unsubscribe(EventSubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void Notify(string subscriptionEvent)
    {
        Debug.Log("EventSubscriber Notify: " + subscriptionEvent);

        foreach (EventSubscriber subscriber in subscribers)
        {
            subscriber.React(subscriptionEvent);
        }
    }

    public void Notify(string subscriptionEvent, params object[] parameters)
    {
        string parameterString = "";

        foreach (object parameter in parameters)
        {
            parameterString += " " + parameter.ToString();
        }

        Debug.Log("EventSubscriber Notify: " + subscriptionEvent + parameterString);

        foreach (EventSubscriber subscriber in subscribers)
        {
            subscriber.React(subscriptionEvent, parameters);
        }
    }
}