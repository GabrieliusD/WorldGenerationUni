using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStone : Interactable
{
    void Start()
    {
        resource = ResourceType.Stone;
    }
    public override void Interact()
    {
        Debug.Log("Interacting with the Stone");
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<unit>().GatherResource(resource);
        }
    }
}
