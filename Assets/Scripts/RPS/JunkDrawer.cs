using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomerServiceCombat
{
    public class JunkDrawer : MonoBehaviour
    {
        public Sprite[] junkItems;

        List<int> junkPool; // contains index(es) to available junk items
        Image image;
        Button button;

        void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
            junkPool = new List<int>(junkItems.Length);
            
            for (int i = 0; i < junkItems.Length; ++i)
            {
                junkPool.Add(i);
            }

            GenerateItem();
        }

        void OnEnable()
        {
            button.onClick.AddListener(UseItem);
        }

        void onDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        void GenerateItem()
        {
            if (junkPool.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, junkPool.Count);
            int itemIndex = junkPool[randomIndex];

            image.sprite = junkItems[itemIndex];
            junkPool.RemoveAt(randomIndex);
        }

        void UseItem()
        {
            // TOOD: implement item effects
            // image.enabled = false;

            GenerateItem();
        }
    }
}