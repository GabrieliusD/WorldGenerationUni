using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mercenary : BuildingBase
{
    public GameObject buttonToCreate;
    public float radius = 6;
    public Animator animator;
    public GameObject[] soldiers;
    Button button;
    bool soldierBought = false;
    public LayerMask SphereCheck;
    public LayerMask terrain;
    static GameObject currentSoldier;

    Queue<GameObject> createdButtons = new Queue<GameObject>();
    TabButton tabButton;
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.AIPlayer)
        {
            this.gameObject.AddComponent<AIMercenary>();
            Animator animator = this.gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AIMercenaryStates", typeof(RuntimeAnimatorController));
        }else 
        {
            tabButton = gameObject.AddComponent<TabButton>();
            tabButton.tabID = 5;
            tabButton.tabGroup = FindObjectOfType<TabGroup>();
        }
        
    }
    public void purchase(GameObject soldier)
    {
        if(soldier == null)
        {
            soldier = soldiers[0];
        }
        int loops = 0;
        soldierBought = false;
        Debug.Log("Recruit Soldier");
        int goldCost = soldier.GetComponent<UnitObjectParameter>().goldCost;
        if(ResourceManager.Instance.PurchaseSoldier(goldCost, PlayerType))
        while(!soldierBought)
        {
            Vector3 centre = transform.position;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 5, randomPos.y);
            RaycastHit hit;
            if(Physics.Raycast(v, Vector3.down, out hit,6f,terrain))
            {
                if(!Physics.CheckSphere(hit.point, 4.0f, SphereCheck))
                {
                    GameObject s = Instantiate(soldier,hit.point,Quaternion.identity);
                    
                    soldierBought = true;
                    currentSoldier = null;
                    if(PlayerType == PlayerTypes.AIPlayer)
                    {
                        s.tag = "Enemy";
                        s.GetComponent<UnitBase>().PlayerType = PlayerTypes.AIPlayer;
                        s.GetComponent<Animator>().enabled = true;
                        GetComponent<AIMercenary>().addSoldierToTheList(s.GetComponent<Soldier>());
                        
                    } else s.tag = "Player";
                        

                }
            }
            loops++;

        }
            Debug.Log("Num Loops: " + loops);
    }

    public void setSoldier(GameObject soldier)
    {
        currentSoldier = soldier;
        Interaction();
    }

    public void InstatiateButtons()
    {
        while(createdButtons.Count > 0)
        {
            GameObject createdButton = createdButtons.Dequeue();
            Destroy(createdButton);
        }
        foreach (GameObject soldier in soldiers)
        {
            ButtonToCreateText buttonTexts = buttonToCreate.GetComponent<ButtonToCreateText>();
            int goldCost = soldier.GetComponent<UnitObjectParameter>().goldCost;
            string name = soldier.GetComponent<UnitObjectParameter>().soldierName;
            buttonTexts.soldierName.text = name;
            buttonTexts.soldierCost.text = "Gold: " + goldCost;
            
            GameObject buttonCreated = Instantiate(buttonToCreate,BuildMenuNavigation.Instance.ButtonInitLocation.transform);
            buttonCreated.GetComponent<Button>().onClick.AddListener(delegate{purchase(soldier);});
            createdButtons.Enqueue(buttonCreated);
        }

    }
    public override void EnableMenu()
    {
        tabButton.OnPointerClick(null);
        InstatiateButtons();
        //BuildMenuNavigation.Instance.EnableMercenaryMenu();
        // button = BuildMenuNavigation.Instance.purchaseSoldier.GetComponent<Button>();
        // button.onClick.RemoveAllListeners();
        // button.onClick.AddListener(Interaction);
    }

    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.ResetMenus();
        //button.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        if(trackIfDestroyed !=null)
        trackIfDestroyed.SetBool("mercenary", false);
    }
}
