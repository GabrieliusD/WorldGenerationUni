using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : BuildingBase
{
    ResourceManager resourceManager;
    public override void Start()
    {
       base.Start(); 
       resourceManager = ResourceManager.Instance;
       resourceManager.IncreaseMaxStorage(PlayerType);

    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        resourceManager.DecreaseMaxStorage(PlayerType);
    }
}


