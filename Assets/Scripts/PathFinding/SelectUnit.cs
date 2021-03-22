using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    public LayerMask layer; 
    public List<UnitBase> units;
    
    Rect mouseSelectZone;
    Texture2D pixelTexture;
    Vector2 startMousePos;
    void Start()
    {
        pixelTexture = new Texture2D(1,1, TextureFormat.ARGB32,false);
        pixelTexture.SetPixel(0,0, Color.white);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "unit")
                {
                    unit u = hit.collider.GetComponent<unit>();
                    if(!units.Contains(u))
                        units.Add(u);
                } else{
                    if(units.Count > 0)
                        units.Clear();
                    
                }
            }
  
        }
        SelectTarget();
        InteractWithObject();
    }

    public void SelectUnitsInRect(Rect rect)
    {
        UnitBase[] tempUnits = FindObjectsOfType<UnitBase>();
        foreach (var unit in tempUnits)
        {
            Vector3 screenpoint = Camera.main.WorldToScreenPoint(unit.transform.position);
            Vector3  screenPointMod = new Vector3(screenpoint.x, Screen.height - screenpoint.y, screenpoint.z);
            if(rect.Contains(screenPointMod))
            {
                if(!units.Contains(unit))
                 units.Add(unit);
            }
        }
    }
    Rect rect = new Rect();
    void OnGUI()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startMousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);

        }
        if(Input.GetMouseButton(0))
        {
            Vector2 mPos = new Vector2(-startMousePos.x + Input.mousePosition.x, Screen.height - startMousePos.y - Input.mousePosition.y);
            rect = new Rect(startMousePos.x,startMousePos.y, mPos.x, mPos.y);
            GUI.Box(rect,"");
        }
        if(Input.GetMouseButtonUp(0))
        {
            SelectUnitsInRect(rect);
        }

    }

    void InteractWithObject()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,Mathf.Infinity))
            {
                Interactable inte = hit.collider.GetComponent<Interactable>();
                if(inte != null)
                {
                    foreach(UnitBase u in units)
                    {
                        if(WorkerManager.Instance.AllowWorker(inte.GetResourceType()))
                        {
                            if(u.GetType() == typeof(unit))
                            {
                                u.SetFocus(inte);
                                WorkerManager.Instance.IncreaseCurrentWorkers(inte.GetResourceType());
                            }
                        }
                    }
                } else foreach(UnitBase u in units) u.RemoveFocus();
            }

        }
    }


    void SelectTarget()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(units.Count > 0)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit,Mathf.Infinity, layer))
                {
                    Vector3 target = hit.point;
                    foreach(UnitBase u in units)
                    {
                        u.GetComponent<UnitBase>().IssuePath(target);
                    }
                }
            }
        }
    }
}