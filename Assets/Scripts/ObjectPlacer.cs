using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public LayerMask layer;

    public LayerMask ObjectUnplacableLayers;
    public GameObject testBuilding;
    public GameObject prefab;
    public KeyCode newObjectHotKey;
    ResourceManager rManger;
    private int woodCost;
    private int stoneCost;
    GameObject currentPlaceableObject;
    float mouseWheelRotation;

    void Start()
    {
        rManger = ResourceManager.Instance;
    }
    void Update()
    {
        HandleNewObjectHotkey();
        HoverNewObject();
        
        if(currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            Release();
        }
    }
    public void HoverNewObject()
    {
        if(prefab != null)
        {
            currentPlaceableObject = Instantiate(prefab);
            currentPlaceableObject.GetComponent<BuildingBase>().PlayerType = PlayerTypes.humanPlayer;
            currentPlaceableObject.tag = "Player";
            prefab = null;
        }
    }

    public void setPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
        BuildingObjectParameter op = newPrefab.GetComponent<BuildingObjectParameter>();
        op.playerTypes = PlayerTypes.humanPlayer;
        woodCost = op.GetWoodCost();
        stoneCost = op.GetStoneCost();
    }
    public void Release()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!Physics.CheckSphere(currentPlaceableObject.transform.position, 2.0f, ObjectUnplacableLayers))
            {
                if(rManger.PurchaseBuilding(woodCost, stoneCost, PlayerTypes.humanPlayer))
                {
                    woodCost = 0;
                    stoneCost = 0;
                    currentPlaceableObject = null;
                    prefab = null;
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            prefab = null;
            woodCost = 0;
            stoneCost = 0;
            Destroy(currentPlaceableObject);
        }
    }
    public void HandleNewObjectHotkey()
    {
        if(Input.GetKeyDown(newObjectHotKey))
        {
            if(currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(testBuilding);
            }else{
                Destroy(currentPlaceableObject);
            }
        }
    }

    public void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,100f, layer))
        {
            currentPlaceableObject.transform.position = hit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        }
    }

    public void RotateFromMouseWheel()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10.0f);
        }
    }
}
