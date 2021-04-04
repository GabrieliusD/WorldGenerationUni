using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHudInteract : BuildingBase
{
    // Start is called before the first frame update
    PlayerTypes playerTypes;

    private void Awake()
    {
        PlayerType = GetComponent<BuildingObjectParameter>().playerTypes;
    }
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.StoneHutBuild();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.StoneHutBuild();
    }

    // Update is called once per frame
    void Destroy()
    {
        if(PlayerType == PlayerTypes.humanPlayer)
        WorkerManager.Instance.StoneHutDestroyed();
        if(PlayerType == PlayerTypes.AIPlayer)
        EnemyWorkerManager.Instance.StoneHutDestroyed();
    }
}
