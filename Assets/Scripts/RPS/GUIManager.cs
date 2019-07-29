using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomerServiceCombat
{
    public class GUIManager : MonoBehaviour
    {
        public Slider customerHealth;
        public Slider playerHealth;

        EventSubscriber eventSubscriber;

        void Awake()
        {
            eventSubscriber = GetComponent<EventSubscriber>();
        }

        void OnEnable()
        {
            eventSubscriber.OnReact += React;
        }

        void OnDisable()
        {
            eventSubscriber.OnReact -= React;
        }

        void React(string eventName, object[] parameters)
        {
            switch(eventName)
            {
                case "CUSTOMER_SET_HEALTH":
                {
                    int health = (int) parameters[0];
                    
                    customerHealth.maxValue = health;
                    customerHealth.minValue = 0;
                    customerHealth.value = health;
                    break;
                }

                case "CUSTOMER_TAKES_DAMAGE": 
                {
                    int damage = (int) parameters[0];

                    customerHealth.value -= damage;
                    break;
                }

                case "PLAYER_TAKES_DAMAGE":
                {
                    int damage = (int) parameters[0];

                    playerHealth.value -= damage;
                    break;
                }
            }
        }
    }
}