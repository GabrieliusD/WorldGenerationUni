using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MarketUI : MonoBehaviour
{
    bool woodActive;
    bool stoneActive;
    bool metalActive;

    public void woodButtonPressed()
    {
        woodActive = true;
        stoneActive = false;
        metalActive = false;
    }
    public void stoneButtonPressed()
    {
        woodActive = false;
        stoneActive = true;
        metalActive = false;
    }
    public void metalButtonPressed()
    {
        woodActive = false;
        stoneActive = false;
        metalActive = true;
    }

    public void buyResource()
    {
        if(woodActive)
        {
            ResourceManager.Instance.buyResource(ResourceType.Wood, 10, PlayerTypes.humanPlayer);
        }
        if(stoneActive)
        {
            ResourceManager.Instance.buyResource(ResourceType.Stone, 10, PlayerTypes.humanPlayer);           
        }
        if(metalActive)
        {
            ResourceManager.Instance.buyResource(ResourceType.Metal, 10, PlayerTypes.humanPlayer);
        }
    }
    public void sellResource()
    {
        if(woodActive)
        {
            ResourceManager.Instance.sellResource(ResourceType.Wood, 10, PlayerTypes.humanPlayer);
        }
        if(stoneActive)
        {
            ResourceManager.Instance.sellResource(ResourceType.Stone, 10, PlayerTypes.humanPlayer);           
        }
        if(metalActive)
        {
            ResourceManager.Instance.sellResource(ResourceType.Metal, 10, PlayerTypes.humanPlayer);
        }
    }
}
