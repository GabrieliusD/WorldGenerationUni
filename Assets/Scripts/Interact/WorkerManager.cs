using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager Instance{get; private set;}
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    int maxWoodWorkers;
    int maxStoneWorkers;
    int maxMetalWorkers;

    int currentWoodWorkers;
    int currentStoneWorkers;
    int currentMetalWorkers;

    public void WoodHutBuild()
    {
        maxWoodWorkers++;
    }

    public void woodHutDestroyed()
    {
        maxWoodWorkers--;
    }

    public void StoneHutBuild()
    {
        maxStoneWorkers++;
    }

    public void StoneHutDestroyed()
    {
        maxStoneWorkers--;
    }

    public void MetalHutBuild()
    {
        maxMetalWorkers++;
    }
    public void MetalHutDestroyed()
    {
        maxMetalWorkers--;
    }

    public void IncreaseCurrentWorkers(ResourceType resource)
    {
        if(resource == ResourceType.Wood)
            currentWoodWorkers++;
        if(resource == ResourceType.Stone)
            currentStoneWorkers++;
        if(resource == ResourceType.Metal)
            currentMetalWorkers++;
    }

    public void DecreaseCurrentWorkers(ResourceType resource)
    {
        if(resource == ResourceType.Wood)
            currentWoodWorkers--;
        if(resource == ResourceType.Stone)
            currentStoneWorkers--;
        if(resource == ResourceType.Metal)
            currentMetalWorkers--;
    }

    public bool AllowWorker(ResourceType resource)
    {
        if(resource == ResourceType.Wood)
            return maxWoodWorkers - currentWoodWorkers > 0;
        if(resource == ResourceType.Stone)
            return maxStoneWorkers - currentStoneWorkers > 0;
        if(resource == ResourceType.Metal)
            return maxMetalWorkers - currentMetalWorkers > 0;
        return false;
    }
}
