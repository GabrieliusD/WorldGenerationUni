using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTree : Interactable
{

    void Start()
    {
        resource = ResourceType.Wood;
    }
    public override void Interact()
    {
        Debug.Log("Interacting with the tree");
        for (int i = 0; i < players.Count; i++)
        {
            UnitBase u = players[i].GetComponent<unit>();
            if(u.GetType() == typeof(unit))
                players[i].GetComponent<unit>().GatherResource(resource);
        }
    }

}
