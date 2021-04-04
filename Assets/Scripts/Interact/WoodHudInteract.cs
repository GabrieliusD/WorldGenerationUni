using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodHudInteract : BuildingBase
{
    // Start is called before the first frame updateZ
    private void Awake()
    {
        PlayerType = GetComponent<BuildingObjectParameter>().playerTypes;
    }
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.WoodHutBuild();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.WoodHutBuild();
    }

    void Destroy()
    {
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.woodHutDestroyed();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.woodHutDestroyed();
    }
}
