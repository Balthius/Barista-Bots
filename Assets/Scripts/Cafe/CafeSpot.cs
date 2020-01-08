using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeSpot : MonoBehaviour
{
    const float FRIENDSHIP_BOOST = 0.1f;
    const float UPGRADE_BOOST = 0.1f;

    public string id;
    public float baseChance = 0.1f;
    public float maxFriendship;
    
    // only used for debugging
    public bool debug = false;
    public bool isEmpty;
    public bool isMessy;
    public bool isUpgraded;

    [System.Serializable]
    public class CafeSpotData
    {
        public long timestamp = DateTime.UtcNow.Ticks;    // just do this once for all spots
        public int friendship = 0;
        public bool isEmpty = true;
        public bool isMessy = false;
        public bool isUpgraded = false;
    }
    public CafeSpotData data;

    private Animator animator;
    private EventSubscriber subscriber;
    private SpriteRenderer spriteRenderer;
    private Transform bot;

    void Awake ()
    {
        animator = this.GetComponent<Animator>();
        subscriber = this.GetComponent<EventSubscriber>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        bot = this.transform.GetChild(0);
        OnInitialize();
    }

    void OnEnable ()
    {
        subscriber.OnReact += OnReact;
    }

    void OnDisable ()
    {
        subscriber.OnReact -= OnReact;
    }

    void OnReact (string eventName, object[] parameters)
    {
    }

    void OnInitialize ()
    {
        if (debug)
        {
            Debug.LogError(id + " in debug mode");
            data = new CafeSpotData();
            data.isEmpty = isEmpty;
            data.isMessy = isMessy;
            data.isUpgraded = isUpgraded;
            UpdateState();
            return;
        }

        // instead of doing this on per spot basis, consolidate in one place
        string jsonString = PlayerPrefs.GetString(id, "");

        // if (debug) { Debug.LogError(id + " OnInitialize --> " + jsonString); }

        data = jsonString.Length != 0
            ? JsonUtility.FromJson<CafeSpotData>(jsonString)
            : new CafeSpotData();

        DateTime lastSaved = new DateTime(data.timestamp);
        // double deltaTime = DateTime.UtcNow.Subtract(lastSaved).TotalHours;
        double deltaTime = DateTime.UtcNow.Subtract(lastSaved).TotalMinutes;
        double refreshCycles = Math.Floor(deltaTime);

        // if (debug) { Debug.LogError(DateTime.UtcNow + " - " + lastSaved + " = " + refreshCycles); }

        if (refreshCycles > 0 && !data.isEmpty)
        {
            bool shouldBeEmpty = ShouldBeEmpty();

            data.isEmpty = shouldBeEmpty;
            data.isMessy = shouldBeEmpty;   // bot left, leave a mess

            --refreshCycles;

            // if (debug) { Debug.LogError(id + " cycle 1 --> " + shouldBeEmpty); }
        }

        while (refreshCycles > 0 && !data.isMessy)
        {
            data.isEmpty = ShouldBeEmpty();

            --refreshCycles;
            
            // if (debug) { Debug.LogError(id + " cycles --> " + data.isEmpty); }
        }

        UpdateState();
    }

    int countCycles (long ticksLastSaved)
    {
        DateTime lastSaved = new DateTime(ticksLastSaved);
        int minutes = lastSaved.Minute;
        int seconds = lastSaved.Second;
        int milliseconds = lastSaved.Millisecond;
        
        // DateTime lastHour = lastSaved.AddMinutes(-minutes).AddSeconds(-seconds).AddMilliseconds(-milliseconds); // floor to last hour
        DateTime lastHour = lastSaved.AddSeconds(-seconds).AddMilliseconds(-milliseconds); // floor to last minute

        DateTime now = DateTime.UtcNow;
        return (int) now.Subtract(lastHour).TotalHours;
    }

    bool ShouldBeEmpty ()
    {
        float baristaBoost = 0; // figure how this works
        float friendshipBoost = data.friendship == maxFriendship ? FRIENDSHIP_BOOST : 0;
        float upgradeBoost = data.isUpgraded ? UPGRADE_BOOST : 0;
        float totalChance = baseChance + baristaBoost + friendshipBoost + upgradeBoost;
        float lottery = UnityEngine.Random.Range(0f, 1f);

        return lottery >= totalChance;
    }

    void UpdateState ()
    {
        animator.SetBool("isEmpty", data.isEmpty);
        animator.SetBool("isMessy", data.isMessy);
        animator.SetBool("isUpgraded", data.isUpgraded);
        transform.GetChild(0).gameObject.SetActive(!data.isEmpty || data.isMessy);
    }

    void OnDestroy ()
    {
        if (debug)
        {
            return;
        }

        // instead of doing this on per spot basis, consolidate in one place
        data.timestamp = DateTime.UtcNow.Ticks;

        string jsonString = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(id, jsonString);

        // if (debug) { Debug.LogError(id + " OnDestroy --> " + jsonString); }
    }

    public void OnTap()
    {
        if (data.isMessy) {
            data.isMessy = false;
            UpdateState();
        } else if (!data.isEmpty) {
            GetComponentInChildren<CafeBot>().clicked = true;
        }
    }
}
