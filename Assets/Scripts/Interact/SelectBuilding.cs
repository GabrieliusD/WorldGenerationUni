using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    BuildingBase building;
    BuildingBase currentlySelected;
    public LayerMask layer;
    void Update()
    {
        UnSelect();
        GetBuildingFromMouse();
    }

    void GetBuildingFromMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, 100.0f, layer))
            {
                building = hit.collider.GetComponent<BuildingBase>();
                if(building != null && building != currentlySelected)
                {
                    PlayerTypes pt = building.PlayerType;
                    if(pt == PlayerTypes.humanPlayer)
                    {
                        building.EnableMenu();
                        currentlySelected = building;
                    }
                } 
                
            } 
        }
    }

    void UnSelect()
    {
        if(Input.GetMouseButtonDown(1))
        {if(building != null)
            building.DisableMenu();
            currentlySelected = null;
        }
    }
}
