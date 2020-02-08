using System;
using UnityEngine;

public class CafeSpot : MonoBehaviour
{
    const float FRIENDSHIP_BOOST = 0.1f;
    const float UPGRADE_BOOST = 0.1f;

    public string id;
    public float baseChance = 0.1f;
    public float maxFriendship;
    
    // only used for debugging
    public bool useMinutes = false;
    public bool logging = false;
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

    public void Initialize ()
    {
        if (debug) {
            Debug.Log(id + " in debug mode");
            data = new CafeSpotData();
            data.isEmpty = isEmpty;
            data.isMessy = isMessy;
            data.isUpgraded = isUpgraded;
            UpdateState();
            return;
        }

        // instead of doing this on per spot basis, consolidate in one place
        var jsonString = PlayerPrefs.GetString(id, "");

        if (logging) { Debug.Log(id + " OnInitialize --> " + jsonString); }

        data = jsonString.Length != 0
            ? JsonUtility.FromJson<CafeSpotData>(jsonString)
            : new CafeSpotData();

        DateTime lastSaved = new DateTime(data.timestamp);
        var now = DateTime.UtcNow;
        data.timestamp = new DateTime(now.Year, now.Month, now.Day, now.Hour, useMinutes ? now.Minute : 0, 0).Ticks;
        var deltaTime = useMinutes
            ? now.Subtract(lastSaved).TotalMinutes
            : now.Subtract(lastSaved).TotalHours;
        var refreshCycles = Math.Floor(deltaTime);

        if (logging) { Debug.Log(id + " " + DateTime.UtcNow + " - " + lastSaved + " = " + refreshCycles); }

        double cyclesPassed = 1;
        while (refreshCycles > 0 && !data.isMessy)
        {
            var botLeft = DidBotLeave();
            
            if (logging) {
                var nextCycle = useMinutes ? lastSaved.AddMinutes(cyclesPassed) : lastSaved.AddHours(cyclesPassed);
                ++cyclesPassed;
                var botAction = botLeft
                    ? string.Format("{0}", (data.isEmpty == botLeft) ? "no show" : "left a tip")
                    : string.Format("{0}", (data.isEmpty == botLeft ? "stayed" : "arrived"));
                Debug.Log(string.Format("{0} [{1:d2}:{2:d2}] {3}", id, nextCycle.Hour, nextCycle.Minute, botAction));
            }

            data.isEmpty = botLeft;
            data.isMessy = botLeft;
            --refreshCycles;
        }

        UpdateState();
    }

    public void UpdateStateFromMiniGame (int score)
    {
        if (!data.isEmpty || data.isMessy) {
            return;
        }

        // bots can only arrive after finishing a minigame, not leave
        while (score > 0)
        {
            var botLeft = DidBotLeave();
            if (!botLeft) {
                data.isEmpty = botLeft;
                if (logging) { Debug.Log(string.Format("{0} arrived", id)); }
                break;
            }
            --score;
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

    bool DidBotLeave ()
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

        string jsonString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(id, jsonString);

        if (logging) { Debug.Log(id + " OnDestroy --> " + jsonString); }
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
