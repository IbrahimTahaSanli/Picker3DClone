using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlatforms : MonoBehaviour
{
    public static CurrentPlatforms _instance;

    [SerializeField] private List<PlatformDetails> platforms;



    public delegate void PlatformAddedEvent(PlatformDetails platformAdded);
    //Boolean for temporary disable event
    private Dictionary<PlatformAddedEvent, bool> PlatformAddedEvents;
    public void AddPlatformAddedEvent(PlatformAddedEvent even, bool isSuspended = false)
    {
        PlatformAddedEvents[even] = isSuspended;
    }
    public void SetSuppressionAddedEvent(PlatformAddedEvent even, bool isSuspended)
    {
        PlatformAddedEvents[even] = isSuspended;
    }
    public void RemovePlatformAddedEvent(PlatformAddedEvent even)
    {
        PlatformAddedEvents.Remove(even);
    }


    public void AddPlatform(PlatformDetails platform)
    {
        this.platforms.Add(platform);

        foreach (PlatformAddedEvent eve in this.PlatformAddedEvents.Keys)
            if (!this.PlatformAddedEvents[eve])
                eve(platform);
    }

    public PlatformDetails[] GetPlatformDetails()
    {
        return this.platforms.ToArray();
    }

    public PlatformDetails[] GetPlatformDetails(int start, int end)
    {
        return this.platforms.GetRange(start, end).ToArray();
    }

    public int GetPlatformCount()
    {
        return this.platforms.Count;
    }


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        this.PlatformAddedEvents = new Dictionary<PlatformAddedEvent, bool>();
    }

}
