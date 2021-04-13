using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerTypes {humanPlayer = 0, AIPlayer = 1, playerTotal = 2}
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance{get; private set;}

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public int[] setWood = new int[2];
    public int[] setStone = new int[2];
    public int[] setMetal = new int[2];
    public int[] setGold = new int[2];

    int woodCost = 10;
    int stoneCost = 30;

    int metalCost = 50;
    const int numPlayers = (int)PlayerTypes.playerTotal;
    int[] wood = new int[numPlayers]; 
    int[] stone = new int[numPlayers];
    int[] metal = new int[numPlayers];
    int[] gold = new int[numPlayers];

    void Start()
    {
        ConfigureResourceSettings();
    }

    public void setUpTest(int value)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            wood[i] = value;
            stone[i] = value;
            gold[i] = value;
            metal[i] = value;
        }
    }

    public void ConfigureResourceSettings()
    {
        WorldSettings ws = WorldSettings.Instance;
        StartingResources pR = ws.GetDifficulty().playerResources;
        wood[0] = pR.wood;
        stone[0] = pR.stone;
        metal[0] = pR.metal;
        gold[0] = pR.gold;        

        StartingResources aiR = ws.GetDifficulty().aiResources;
        wood[1] = aiR.wood;
        stone[1] = aiR.stone;
        metal[1] = aiR.metal;
        gold[1] = aiR.gold;
    }

    public void DepositResource(ResourceType resourceType, int amount, PlayerTypes playerType)
    {
        int pt = (int)(playerType);
        if(resourceType == ResourceType.Wood)
        {
            wood[pt] += amount;
        }
        if(resourceType == ResourceType.Stone)
        {
            stone[pt] += amount;
        }
        if(resourceType == ResourceType.Metal)
        {
            metal[pt] += amount;
        }
    }

    public void buyResource(ResourceType resourceType, int amount, PlayerTypes playerType)
    {
        int pt = (int)(playerType);
        if(resourceType == ResourceType.Wood)
        {
            if(gold[pt] >= woodCost)
            {
                wood[pt] += amount;
                gold[pt] -= woodCost;
            }
        }
        if(resourceType == ResourceType.Stone)
        {
            if(gold[pt] >= stoneCost)
            {
                stone[pt] += amount;
                gold[pt] -= stoneCost;
            }
        }
        if(resourceType == ResourceType.Metal)
        {
            if(gold[pt] >= metalCost)
            {
                metal[pt] += amount;
                gold[pt]-= metalCost;
            }
        }
    }
    public void sellResource(ResourceType resourceType, int amount, PlayerTypes playerType)
    {
        int pt = (int)(playerType);

        if(resourceType == ResourceType.Wood)
        {
            if(wood[pt] >= amount)
            {
                wood[pt] -= amount;
                gold[pt] += woodCost;
            }
        }
        if(resourceType == ResourceType.Stone)
        {
            if(stone[pt] >= amount)
            {
                stone[pt] -= amount;
                gold[pt] += stoneCost;
            }
        }
        if(resourceType == ResourceType.Metal)
        {
            if(metal[pt] >= amount)
            {
                metal[pt] -= amount;
                gold[pt]+= metalCost;
            }
        }
    }
    public bool PurchaseBuilding(int buildingWoodCost, int buildingStoneCost, PlayerTypes playerType)
    {
        int pt = (int)playerType;                

        if(buildingWoodCost <= wood[pt] && buildingStoneCost <= stone[pt])
        {
            wood[pt] -= buildingWoodCost;
            stone[pt] -= buildingStoneCost;
            return true;
        } else return false;
    }

    public bool PurchaseSoldier(int goldCost, PlayerTypes playerType)
    {
        int pt = (int)playerType;                

        if(goldCost <= gold[pt])
        {
            gold[pt] -= goldCost;
            return true;
        } else return false;
    }
    public int GetWood(PlayerTypes playerType)
    {
        return wood[(int)playerType];
        
    } 
    public void SetWood(int amount, PlayerTypes playerType)
    {
        wood[(int)playerType] = amount;
    } 

    public int GetStone(PlayerTypes playerType)
    {
        return stone[(int)playerType];
    }
    public void SetStone(int amount, PlayerTypes playerType)
    {
        stone[(int)playerType] = amount;
    }

    public int GetMetal(PlayerTypes playerType)
    {
        return metal[(int)playerType];
    }
    public void SetMetal(int amount, PlayerTypes playerType)
    {
        metal[(int)playerType] = amount;
    }
    public int GetGold(PlayerTypes playerType)
    {
        return gold[(int)playerType];
    }
    public void SetGold(int amount, PlayerTypes playerType)
    {
        gold[(int)playerType] = amount;
    }

}
