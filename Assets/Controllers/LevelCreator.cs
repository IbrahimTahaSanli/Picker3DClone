using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator _instance;

    [SerializeField] Transform parentOfPlatforms;
    
    [SerializeField] GameObject startPlatform;
    
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject phasePlatform;
    
    [SerializeField] GameObject finishPlatform;
    [SerializeField] GameObject nextLevelPlatform;


    private void Awake()
    {
        _instance = this;
    }

    public void CreateStart()
    {
        GameObject start = Instantiate(this.startPlatform);
        start.transform.SetParent(this.parentOfPlatforms);
        CurrentPlatforms._instance.AddPlatform( start.GetComponent<PlatformDetails>());
    }

    public void CreateLevel(int phaseCount, int platformCount)
    {
        for (int i = 0; i < phaseCount-1; i++)
            CreatePhase(platformCount);

        CreateFinish(platformCount);
    }

    public void CreatePhase(int platformCount)
    {
        PlatformDetails[] platforms = createplatforms(platformCount);
        uint phaseBallCount = 0;
        
        foreach (PlatformDetails platform in platforms)
        {
            platform.transform.SetParent(this.parentOfPlatforms);
            CurrentPlatforms._instance.AddPlatform(platform);
            phaseBallCount += platform.ballCount;
        }

        GameObject phaseEnd = Instantiate(this.phasePlatform);
        phaseEnd.transform.SetParent(this.parentOfPlatforms);
        phaseEnd.GetComponent<PhasePlatformController>().setBallInPhase(phaseBallCount);
        CurrentPlatforms._instance.AddPlatform(phaseEnd.GetComponent<PlatformDetails>());

        GameObject nextPhase = Instantiate(this.startPlatform);
        nextPhase.transform.SetParent(this.parentOfPlatforms);
        CurrentPlatforms._instance.AddPlatform(nextPhase.GetComponent<PlatformDetails>());

        AlignPlatforms(CurrentPlatforms._instance.GetPlatformDetails(CurrentPlatforms._instance.GetPlatformCount() - 1 - platformCount - 2, platformCount + 1 + 2));
    }

    public void CreateFinish(int platformCount)
    {
        PlatformDetails[] platforms = createplatforms(platformCount);
        uint phaseBallCount = 0;

        foreach (PlatformDetails platform in platforms)
        {
            platform.transform.SetParent(this.parentOfPlatforms);
            CurrentPlatforms._instance.AddPlatform(platform);
            phaseBallCount += platform.ballCount;
        }

        GameObject finish = Instantiate(this.finishPlatform);
        finish.transform.SetParent(this.parentOfPlatforms);
        finish.GetComponent<PhasePlatformController>().setBallInPhase(phaseBallCount);
        CurrentPlatforms._instance.AddPlatform(finish.GetComponent<PlatformDetails>());

        GameObject nextLevel = Instantiate(this.nextLevelPlatform);
        nextLevel.transform.SetParent(this.parentOfPlatforms);
        CurrentPlatforms._instance.AddPlatform(nextLevel.GetComponent<PlatformDetails>());

        AlignPlatforms(CurrentPlatforms._instance.GetPlatformDetails(CurrentPlatforms._instance.GetPlatformCount() - 1 - platformCount - 2, platformCount + 1 + 2));
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
            lastPosition += platforms[i - 1].GetComponent<MeshFilter>().mesh.bounds.extents.x + platforms[i].GetComponent<MeshFilter>().mesh.bounds.extents.x;
            platforms[i].transform.position = new Vector3(lastPosition, 0, 0);
        }

    }
}
