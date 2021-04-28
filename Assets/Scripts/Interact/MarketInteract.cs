using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInteract : BuildingBase
{
    TabButton tabButton;
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.AIPlayer) 
        {
            gameObject.AddComponent<AIMarket>();
        } else
        {
            tabButton = gameObject.AddComponent<TabButton>();
            tabButton.tabID = 4;
            tabButton.tabGroup = FindObjectOfType<TabGroup>();
        }
    }
    public override void EnableMenu()
    {
        tabButton.OnPointerClick(null);
    }
    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.ResetMenus();
    }
}
