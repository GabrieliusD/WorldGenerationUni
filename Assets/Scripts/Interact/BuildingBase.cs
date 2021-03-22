using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    bool isSelected = false;
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
