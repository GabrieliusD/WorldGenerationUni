using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectPlacer : MonoBehaviour
{
    public LayerMask layer;
    public Vector2 objectSize = new Vector2(6,6);
    public LayerMask ObjectUnplacableLayers;
    public GameObject testBuilding;
    public GameObject prefab;
    public KeyCode newObjectHotKey;
    ResourceManager rManger;
    private int woodCost;
    private int stoneCost;
    GameObject currentPlaceableObject;
    float mouseWheelRotation;

    public GameObject panel;
    public Text showPriceText;

    Grid grid;

    void Start()
    {
        rManger = ResourceManager.Instance;
        grid = FindObjectOfType<Grid>();
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
        else hideCost();
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
    void showCost(BuildingObjectParameter obp)
    {
        panel.SetActive(true);
        showPriceText.text = "";
        if(obp.woodCost > 0)
        {
            showPriceText.text += "Wood: " + obp.woodCost + "/" + rManger.GetWood(PlayerTypes.humanPlayer) + "  ";
        }
        if(obp.stoneCost > 0)
        {
            showPriceText.text += "Stone: " + obp.stoneCost + "/" + rManger.GetStone(PlayerTypes.humanPlayer) + "  ";
        }

    }

    void hideCost()
    {
        panel.SetActive(false);
    }
    public void setPrefab(GameObject newPrefab)
    {
        if(currentPlaceableObject != null) Destroy(currentPlaceableObject);

        prefab = newPrefab;
        BuildingObjectParameter op = newPrefab.GetComponent<BuildingObjectParameter>();
        showCost(op);
        op.playerTypes = PlayerTypes.humanPlayer;
        woodCost = op.GetWoodCost();
        stoneCost = op.GetStoneCost();
    }

    public void Release()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            if(grid.checkNodesAreEmpty(currentPlaceableObject))
            {
                if(rManger.PurchaseBuilding(woodCost, stoneCost, PlayerTypes.humanPlayer))
                {
                    grid.SetNodeUnwakable(currentPlaceableObject);
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
