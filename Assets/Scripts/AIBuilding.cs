using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuilding : MonoBehaviour
{
    bool isBuild = false;
    public LayerMask SphereCheck;
    EnemyWorkerManager enManager;
    Transform mLocation;
    public List<Building> buildingData;

    public BuildingInteract enemyHallInt;
    Dictionary<string, Building> buildingByName = new Dictionary<string, Building>();
    List<string> buildings = new List<string>() { "hut", "mercenary" };
    void setSpawnLocation(Transform location)
    {
        mLocation = location;
    }
    void createDictionary()
    {
        for (int i = 0; i < buildingData.Count; i++)
        {
            buildingByName.Add(buildingData[i].name, buildingData[i]);
        }
    }
    private void Start()
    {

        enManager = EnemyWorkerManager.Instance;
        setSpawnLocation(this.transform);
        createDictionary();
        buildResourcesCollection();
        StartCoroutine(lateStart(3f));
    }

    IEnumerator lateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        recruitWoodWorkers();
        recruitStoneWorkers();
        recruitMetalWorkers();
        build("mercenary");
    }

    public void buildResourcesCollection()
    {
        build("WoodHut");
        build("StoneHut");
        build("MetalHut");
    }
    public void build(string BuildingName)
    {
        Building b = buildingByName[BuildingName];
        if (b != null)
            build(b);
        else Debug.Log("Building doesnt exist");
    }
    public void build(Building building)
    {
        isBuild = false;
        Debug.Log("AI Building");
        while (!isBuild)
        {
            Vector3 centre = transform.position;
            int radius = 20;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
            RaycastHit hit;
            if (Physics.Raycast(v, Vector3.down, out hit, 20.0f))
            {
                if (!Physics.CheckSphere(v, 10.0f, SphereCheck))
                {
                    building.buildingType.GetComponent<BuildingObjectParameter>().playerTypes = PlayerTypes.AIPlayer;
                    building.buildingType.tag = "Enemy";
                    Instantiate(building.buildingType, hit.point, Quaternion.identity);
                    isBuild = true;
                }
            }
        }
    }

    void recruitWorkerWithTag(string tag)
    {
        enemyHallInt.Interaction();
        GameObject k = enemyHallInt.SpawnedWorker();
        Collider[] objectsInRadius = Physics.OverlapSphere(k.transform.position, 100);
        foreach (var o in objectsInRadius)
        {
            if (o.tag == tag)
            {
                k.GetComponent<unit>().SetFocus(o.GetComponent<Interactable>());
                k.GetComponent<unit>().playerTypes = PlayerTypes.AIPlayer;
                k.tag = "Enemy";
                break;
            }
        }
    }
    public void recruitWoodWorkers()
    {
        if (enManager.AllowWorker(ResourceType.Wood))
        {
            recruitWorkerWithTag("tree");
        }
    }

    public void recruitStoneWorkers()
    {
        if (enManager.AllowWorker(ResourceType.Stone))
        {
            recruitWorkerWithTag("stone");
        }
    }
    public void recruitMetalWorkers()
    {
        if (enManager.AllowWorker(ResourceType.Metal))
        {
            recruitWorkerWithTag("metal");
        }
    }


}
