using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuNavigation : MonoBehaviour
{
    public static BuildMenuNavigation Instance{get; private set;}
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Transform ProductionPanel;
    public Transform MillitaryPanel;
    public Transform ButtonInitLocation;
    public Transform ConstructionPanel;
    public Transform TownHallMenu;

    public Transform MarketMenu;
    public Transform MercnerayMenu;
    public Button purchaseWorker;

    public Button purchaseSoldier;

    public void ResetMenus()
    {
        ProductionPanel.gameObject.SetActive(true);
        MillitaryPanel.gameObject.SetActive(false);
        ConstructionPanel.gameObject.SetActive(false);
        TownHallMenu.gameObject.SetActive(false);
        MarketMenu.gameObject.SetActive(false);
        MercnerayMenu.gameObject.SetActive(false);

    }

}
