﻿using System.Collections;
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
                if(hit.collider.tag == "Player")
                {
                    UnitBase u = hit.collider.GetComponent<UnitBase>();
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
                if(unit.tag == "Player")
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
                if(hit.collider.tag != "Player")
                {
                Interactable[] inte = hit.collider.GetComponents<Interactable>();
                if(inte != null)
                {
                    foreach(UnitBase u in units)
                    {
                        foreach(Interactable i in inte)
                        {
                            if(WorkerManager.Instance.AllowWorker(i.GetResourceType()))
                            {
                                if(u.GetType() == typeof(unit) && i.GetType() != typeof(InteractAttackable))
                                {
                                    u.SetFocus(i);
                                    WorkerManager.Instance.IncreaseCurrentWorkers(i.GetResourceType());
                                }
                            }
                            
                            if(u.GetType() == typeof(Soldier) && i.GetType() == typeof(InteractAttackable))
                            {
                                u.SetFocus(i);
                            }
                        }
                    }
                } else foreach(UnitBase u in units) u.RemoveFocus();
                }
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
                    units.RemoveAll((item => item == null));
                    List<Vector3> targetPositionList = GetPositionListAround(target, new float[]{2,4,6}, new int[] {5, 7, 10});
                    int posIndex = 0;
                    foreach(UnitBase u in units)
                    {
                        u.GetComponent<UnitBase>().IssuePath(targetPositionList[posIndex]);
                        posIndex = (posIndex + 1) % targetPositionList.Count;
                    }
                }
            }
        }
    }
    List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> PositionList = new List<Vector3>();
        PositionList.Add(startPosition);
        for(int i =0; i<ringDistanceArray.Length; i++)
        {
            PositionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return PositionList;
    }
    List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1,0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0,angle,0) * vec;
    }
}