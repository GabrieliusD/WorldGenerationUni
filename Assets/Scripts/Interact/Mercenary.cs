using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mercenary : BuildingBase
{
    public float radius = 6;
    public Animator animator;
    public GameObject soldier;
    Button button;
    bool soldierBought = false;
    public LayerMask SphereCheck;

    static GameObject currentSoldier;
    public override void Start()
    {
        base.Start();
        if(PlayerType == PlayerTypes.AIPlayer)
        {
            this.gameObject.AddComponent<AIMercenary>();
            Animator animator = this.gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AIMercenaryStates", typeof(RuntimeAnimatorController));
        }
    }
    public override void Interaction()
    {
        int loops = 0;
        soldierBought = false;
        Debug.Log("Recruit Soldier");
        int goldCost = soldier.GetComponent<UnitObjectParameter>().goldCost;
        if(ResourceManager.Instance.PurchaseSoldier(goldCost, PlayerType))
        while(!soldierBought)
        {
            Vector3 centre = transform.position;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
            RaycastHit hit;
            if(Physics.Raycast(v, Vector3.down, out hit,20.0f))
            {
                if(!Physics.CheckSphere(hit.point, 1.0f, SphereCheck))
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
    public override void EnableMenu()
    {
        BuildMenuNavigation.Instance.EnableMercenaryMenu();
        button = BuildMenuNavigation.Instance.purchaseSoldier.GetComponent<Button>();
        button.onClick.AddListener(Interaction);
    }

    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.EnableProduction();
        button.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        if(trackIfDestroyed !=null)
        trackIfDestroyed.SetBool("mercenary", false);
    }
}
