using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected ResourceType resource = ResourceType.None;
    public float taskLength = 5.0f;
    float elapsedTime = 0;
    public float radius = 3f;
    public List<Transform> players = new List<Transform>();
    //bool isFocus = false;
    //public Transform player;

    public virtual void Interact()
    {
        Debug.Log("Base Interact");
    }

    void Update()
    {
        if(players.Count > 0)
        {
            for (int i = 0; i < players.Count; i++)
            { 
                float distance = Vector3.Distance(transform.position, players[i].transform.position);
                if(distance < radius)
                {
                    elapsedTime += Time.deltaTime;
                    if(elapsedTime>=taskLength)
                    {
                        Interact();
                        elapsedTime = 0;
                    }
                }
            }
        }
    }

    
    public void OnFocused(Transform playerTransform)
    {
        players.Add(playerTransform);
    }

    public void DeFocused(Transform playerTransform)
    {
        players.Remove(playerTransform);
    }

    public virtual ResourceType GetResourceType()
    {
        return resource;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
