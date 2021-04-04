using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalHudInteract : BuildingBase
{

    private void Awake()
    {
        PlayerType = GetComponent<BuildingObjectParameter>().playerTypes;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.MetalHutBuild();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.MetalHutBuild();
    }

    // Update is called once per frame
    void Destroy()
    {
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.MetalHutDestroyed();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.MetalHutDestroyed();;
    }
}
