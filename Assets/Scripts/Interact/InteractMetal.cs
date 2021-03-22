using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMetal : Interactable
{
    void Start()
    {
        resource = ResourceType.Metal;
    }
    public override void Interact()
    {
        Debug.Log("Interacting with the metal");
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<unit>().GatherResource(resource);
        }
    }
}
