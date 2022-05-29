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


    public void Awake()
    {
        _instance = this;

        this.PlatformAddedEvents = new Dictionary<PlatformAddedEvent, bool>();
        
    }
    public void DestroyLevel(int count)
    {
        int listCount = platforms.Count;

        for (int i = 0; i < listCount; i++)
        {
            if (platforms[0].style == PlatformDetails.PlatformStyle.NextLevel)
            {
                count--;
                platforms[0].style = PlatformDetails.PlatformStyle.Empty;
            }

            if (count == 0)
                break;

            GameObject.Destroy(platforms[0].gameObject, 1);
            platforms.Remove(platforms[0]);
        }

    }

    public void Restart()
    {
        int listCount = platforms.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject.DestroyImmediate(platforms[0].gameObject);
            platforms.Remove(platforms[0]);
        }
    }

}
