using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator _instance;

    [SerializeField] Transform parentOfPlatforms;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject startPlatform;

    private void Awake()
    {
        _instance = this;
    }


    public void CreateLevel(int platformCount)
    {
        Debug.Log("asd");
        PlatformDetails[] platforms = createplatforms(platformCount);
        foreach (PlatformDetails platform in platforms)
        {
            platform.transform.SetParent(parentOfPlatforms);
            CurrentPlatforms._instance.AddPlatform(platform);
        }
        GameObject nextLevel = Instantiate(startPlatform);
        nextLevel.transform.SetParent(parentOfPlatforms);
        CurrentPlatforms._instance.AddPlatform(nextLevel.GetComponent<PlatformDetails>());

        AlignPlatforms(CurrentPlatforms._instance.GetPlatformDetails());
    }

    private PlatformDetails[] createplatforms(int count)
    {
        List<PlatformDetails> list = new List<PlatformDetails>();

        for(int i = 0; i < count; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            list.Add(platform.GetComponent<PlatformDetails>());
        }

        return list.ToArray();
    }

    private void AlignPlatforms(PlatformDetails[] platforms)
    {
        float lastPosition = platforms[0].transform.position.x;
        for(int i = 1; i < platforms.Length; i++)
        {
            lastPosition += platforms[i-1].transform.localScale.x / 2 + platforms[i].transform.localScale.x / 2;
            platforms[i].transform.position = new Vector3(lastPosition, 0, 0);
        }

    }
}
