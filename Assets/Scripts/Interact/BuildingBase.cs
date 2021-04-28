using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public Animator trackIfDestroyed;
    public PlayerTypes PlayerType;
    bool isSelected = false;

    public virtual void Start()
    {
        this.gameObject.AddComponent<InteractAttackable>();
        if(GameObjectTracker.Instance != null)
        GameObjectTracker.Instance.AddObject(gameObject);
    }
    public virtual void Interaction()
    {

    }

    public virtual void EnableMenu()
    {

    }
    public virtual void DisableMenu()
    {

    }

    public void Selected(bool selected)
    {
        isSelected = selected;
    }

    public virtual void OnDestroy()
    {
        GameObjectTracker.Instance.RemoveObject(gameObject);
    }

}
