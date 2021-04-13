using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMarket : MonoBehaviour
{
    public int minWood;
    public int minStone;
    public int minMetal;
    AIStateManager aiState;
    ResourceManager resources;
    void Start()
    {
        aiState = AIStateManager.Instance;
        resources = ResourceManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(aiState.GetState() == AIStates.UnderAttack)
        {
            minWood = 10;
            minStone = 10;
            minMetal = 10;
        } else{
            minWood = 200;
            minStone = 100;
            minMetal = 50;
        }

        if(AllowSellWood()) resources.sellResource(ResourceType.Wood, 10, PlayerTypes.AIPlayer);
        if(AllowSellMetal()) resources.sellResource(ResourceType.Metal, 10, PlayerTypes.AIPlayer);
        if(AllowSellStone()) resources.sellResource(ResourceType.Stone, 10, PlayerTypes.AIPlayer);
    }

    public bool AllowSellWood()
    {
        return resources.GetWood(PlayerTypes.AIPlayer) >= minWood ? true : false;
    }

    public bool AllowSellStone()
    {
        return resources.GetStone(PlayerTypes.AIPlayer) >= minStone ? true : false;
    }

    public bool AllowSellMetal()
    {
        return resources.GetMetal(PlayerTypes.AIPlayer) >= minMetal ? true : false;
    }
}
