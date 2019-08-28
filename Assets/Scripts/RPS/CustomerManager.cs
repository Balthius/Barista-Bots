using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomerServiceCombat
{
    public class CustomerManager : MonoBehaviour
    {
        [System.Serializable]
        public class Bot
        {
            public Sprite[] sprites;    // 0: angry, 1: happy, 2: sad
            public int health = 3;
            public int damageTakenPerAttack = 1;
            public int damageDealtPerAttack = 2;
        }

        public Bot[] bots;
        public EventSubscription eventSubscription;
        public Image image;
        public int maxCustomers = -1;

        List<int> customerIndexQueue;    // start backwards
        EventSubscriber eventSubscriber;
        int emotionState;
        int damageTaken = 0;

        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioClip[] clipList;
        [SerializeField] private GameObject endButton;

        private void Awake()
        {
            eventSubscriber = GetComponent<EventSubscriber>();

            // Randomize bot order
            int numBots = bots.Length;
            List<int> indexPool = new List<int>(numBots);
            
            for (int i = 0; i < numBots; ++i)
            {
                indexPool.Add(i);
            }

            customerIndexQueue = new List<int>(numBots);
            maxCustomers = maxCustomers <= 0 || numBots < maxCustomers
                ? numBots
                : maxCustomers;

            int randomIndex;

            for (int i = 0; i < maxCustomers; ++i)
            {
                randomIndex = Random.Range(0, indexPool.Count);
                customerIndexQueue.Add(indexPool[randomIndex]);
                indexPool.RemoveAt(randomIndex);
            }

            RandomizeCustomerEmotion();
        }

        void OnEnable()
        {
            eventSubscriber.OnReact += React;
        }

        void OnDisable()
        {
            eventSubscriber.OnReact -= React;
        }

        void Start()
        {
            int botIndex = customerIndexQueue[customerIndexQueue.Count - 1];
            Bot customer = bots[botIndex];

            eventSubscription.Notify("CUSTOMER_SET_HEALTH", customer.health);
        }

        public void React (string eventName, object[] parameters)
        {
            switch(eventName)
            {
                case "EMOTION_SELECTED":
                    int emotionPlayerSelected = (int)parameters[0];
                    HandleCombat(emotionPlayerSelected);
                    break;
            }
        }

        void HandleCombat(int emotionPlayerSelected)
        {
            int botIndex = customerIndexQueue[customerIndexQueue.Count - 1];
            Bot customer = bots[botIndex];

            if (emotionPlayerSelected == 0 && emotionState == 1
                || emotionPlayerSelected == 1 && emotionState ==  2
                || emotionPlayerSelected == 2 && emotionState == 0)
            {
                // audioManager.PlaySingle(clipList[1]);
                // Debug.LogError("Player won: " + emotionPlayerSelected + " vs " + emotionState);
                damageTaken += customer.damageTakenPerAttack;
                eventSubscription.Notify("CUSTOMER_TAKES_DAMAGE", customer.damageTakenPerAttack);
            }
            else if (emotionPlayerSelected == 2 && emotionState == 1
                || emotionPlayerSelected == 1 && emotionState == 0
                || emotionPlayerSelected == 0 && emotionState == 2)
            {
                // audioManager.PlaySingle(clipList[0]);
                // Debug.LogError("Customer won: " + emotionPlayerSelected + " vs " + emotionState);
                eventSubscription.Notify("PLAYER_TAKES_DAMAGE", customer.damageDealtPerAttack);
            }
            else
            {
                // Debug.LogError("Draw: " + emotionPlayerSelected + " vs " + emotionState);
                // audioManager.PlaySingle(clipList[1]);
            }

            if (damageTaken >= customer.health)
            {
                customerIndexQueue.RemoveAt(customerIndexQueue.Count - 1);
                damageTaken = 0;
                eventSubscription.Notify("CUSTOMER_SET_HEALTH", customer.health);
            }

            if (customerIndexQueue.Count == 0)
            {
                Debug.LogError("Player wins!");
                return;
            }

            RandomizeCustomerEmotion();
        }

        public void RandomizeCustomerEmotion()
        {
            int botIndex = customerIndexQueue[customerIndexQueue.Count - 1];
            var sprites = bots[botIndex].sprites;

            emotionState = Random.Range(0, sprites.Length);
            image.sprite = sprites[emotionState];
        }
    }
}