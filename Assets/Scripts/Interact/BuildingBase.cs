using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public PlayerTypes PlayerType;
    bool isSelected = false;

    public virtual void Start()
    {
        this.gameObject.AddComponent<InteractAttackable>();
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

}
