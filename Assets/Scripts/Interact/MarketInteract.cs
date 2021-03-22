using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInteract : BuildingBase
{
    public override void EnableMenu()
    {
        BuildMenuNavigation.Instance.EnableMarketMenu();
    }
    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.EnableProduction();
    }
}
