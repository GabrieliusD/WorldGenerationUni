using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuildingInteract : BuildingBase
{
    public GameObject victoryPanel;
    Text victoryText;
    GameObject vp;
    public GameObject worker;
    GameObject workerObject;
    Button button;
    bool workerBought = false;
    public LayerMask SphereCheck;
    Grid grid;

    TabButton tabButton;
    public override void Start()
    {
        base.Start();
        Canvas c = GameObject.Find("CanvasUI").GetComponent<Canvas>();
        
        vp = Instantiate(victoryPanel,c.transform);
        
        victoryText = vp.transform.Find("Victory").GetComponent<Text>();
        vp.gameObject.SetActive(false);
        grid = FindObjectOfType<Grid>();

        tabButton = gameObject.AddComponent<TabButton>();
        tabButton.tabID = 3;
        tabButton.tabGroup = FindObjectOfType<TabGroup>();

    }
    public override void Interaction()
    {
        workerBought = false;
        Debug.Log("Recruit worker");
        if(ResourceManager.Instance.PurchaseSoldier(worker.GetComponent<UnitObjectParameter>().goldCost, PlayerType))
        while(!workerBought)
        {
            Vector3 centre = transform.position;
            int radius = 6;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
            RaycastHit hit;
            if(Physics.Raycast(v, Vector3.down, out hit,20.0f))
            {
                bool walkable = grid.NodeFromWorldPoint(hit.point).walkable;
                if(walkable)
                {
                    workerObject = Instantiate(worker,hit.point + Vector3.up * 2,Quaternion.identity);
                    workerObject.tag = "Player";
                    
                    workerBought = true;
                }
            }
        }
    }
    public override void EnableMenu()
    {
        tabButton.OnPointerClick(null);
        button = BuildMenuNavigation.Instance.purchaseWorker.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Interaction);
    }

    public void buttonSelected()
    {
        Debug.Log("button selected");
    }
    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.ResetMenus();
        button.onClick.RemoveAllListeners();
        
    }

    public GameObject SpawnedWorker()
    {
        if(workerObject!= null)
        return workerObject;
        else return null;
    }

    private void OnDestroy()
    {
        if(vp.gameObject != null)
        {
            vp.gameObject.SetActive(true);
            if(PlayerType == PlayerTypes.AIPlayer)
            {
                victoryText.text = "Player has Defeated AI";
            }
            if(PlayerType == PlayerTypes.humanPlayer)
            {
                victoryText.text = "AI has Defeated Player";
            }
        }
    }


}
