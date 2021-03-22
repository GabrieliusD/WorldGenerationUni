using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mercenary : BuildingBase
{
    public GameObject soldier;
    Button button;
    bool soldierBought = false;
    public LayerMask SphereCheck;

    static GameObject currentSoldier;
    public override void Interaction()
    {
        soldierBought = false;
        Debug.Log("Recruit Soldier");
        while(!soldierBought)
        {
            Vector3 centre = transform.position;
            int radius = 5;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
            RaycastHit hit;
            if(Physics.Raycast(v, Vector3.down, out hit,20.0f))
            {
                if(!Physics.CheckSphere(v, 4.0f, SphereCheck))
                {
                    Instantiate(soldier,hit.point + Vector3.up * 2,Quaternion.identity);
                    soldierBought = true;
                    currentSoldier = null;
                }
            }
        }
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
}
