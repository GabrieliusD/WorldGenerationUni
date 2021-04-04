using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingInteract : BuildingBase
{
    public GameObject worker;
    GameObject workerObject;
    Button button;
    bool workerBought = false;
    public LayerMask SphereCheck;
    public override void Interaction()
    {
        workerBought = false;
        Debug.Log("Recruit worker");
        while(!workerBought)
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
                    workerObject = Instantiate(worker,hit.point + Vector3.up * 2,Quaternion.identity);
                    workerObject.tag = "Player";
                    workerBought = true;
                }
            }
        }
    }
    public override void EnableMenu()
    {
        BuildMenuNavigation.Instance.EnableTownhallMenu();
        button = BuildMenuNavigation.Instance.purchaseWorker.GetComponent<Button>();
        button.onClick.AddListener(Interaction);
    }
    public override void DisableMenu()
    {
        BuildMenuNavigation.Instance.EnableProduction();
        button.onClick.RemoveAllListeners();
    }

    public GameObject SpawnedWorker()
    {
        if(workerObject!= null)
        return workerObject;
        else return null;
    }
}
