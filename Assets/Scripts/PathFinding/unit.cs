using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum ResourceType {None, Wood, Stone, Metal};
public struct UnitStorage
{
    int mCapacity;
    int taken;
    ResourceType resourceType;
    public UnitStorage(int capacity)
    {
        mCapacity = capacity;
        taken = 0;
        resourceType = ResourceType.None;
    }

    public void SetResourceType(ResourceType newResourceType)
    {
        if(resourceType != newResourceType)
        {
            DepositStorage();
            resourceType = newResourceType;
        }

        resourceType = newResourceType;
    }

    public ResourceType GetResourceType()
    {
        return resourceType;
    }
    public void Gather(int amount)
    {
        if(!checkFull())
        taken += amount;
    }

    public int getStorage()
    {
        return taken;
    }

    public int DepositStorage()
    {
        int temp = taken;
        taken = 0;
        return temp;
    }

    public bool checkFull()
    {
        if(taken >= mCapacity)
        {
            return true;
        }
        else return false;
    }
}
public class unit : UnitBase
{
    public UnitStorage unitStorage;

    public PlayerTypes playerTypes = PlayerTypes.humanPlayer;
    override public void Start()
    {
        base.Start();
        unitStorage = new UnitStorage(100);
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Soldier destroyed");
        }
        //if(target != null)
        //{
            //IssuePath(target.position);
        //}
        
    }
    public void ResumeLastTask()
    {
        SetFocus(lastFocus);
        IssuePath(lastFocus.transform.position);
    }
    public void GatherResource(ResourceType resourceType)
    {
        unitStorage.SetResourceType(resourceType);
        unitStorage.Gather(50);
        if(unitStorage.checkFull())
        {
            Debug.Log("unit storage is full");
            DepositResource();
        }
    }

    public void DepositResource()
    {
        lastFocus = focus;
        Storage storageLocation = closestStorage();
        SetFocus(storageLocation);
        IssuePath(storageLocation.transform.position);
    }
    public Storage closestStorage()
    {
        float distance = Mathf.Infinity;
        Storage[] s = FindObjectsOfType<Storage>();
        Storage closest = null;
        foreach (var item in s)
        {
            float newDistance = Vector3.Distance(item.transform.position, transform.position);
            if(newDistance < distance)
            {
                distance = newDistance;
                closest = item;
            }
        }

        return closest;
    }

    public override void RemoveFocus()
    {
        if(focus != null)
        {
            focus.DeFocused(transform);
            WorkerManager.Instance.DecreaseCurrentWorkers(unitStorage.GetResourceType());
        }
        focus = null;
        StopTracking();
    }
}
