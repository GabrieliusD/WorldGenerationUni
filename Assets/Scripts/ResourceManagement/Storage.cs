using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable
{
    int storageCapacity = 1000;

    void Start()
    {
        taskLength = 2f;
    }
    
    public override void Interact()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Deposit Wood");
            unit u = players[i].GetComponent<unit>();

            ResourceManager.Instance.DepositResource(u.unitStorage.GetResourceType(), u.unitStorage.DepositStorage(), u.playerTypes);
            u.ResumeLastTask();
        }
    }

}
