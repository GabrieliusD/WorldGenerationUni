using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuilding : MonoBehaviour
{
    public Vector3 DistanceBetweenBuildings;
    public float BuildingRadius;
    public float minDistanceForUnderAttack = 100.0f;
    bool isBuild = false;
    public LayerMask SphereCheck;
    EnemyWorkerManager enManager;
    Transform mLocation;
    public List<Building> buildingData;

    public BuildingInteract enemyHallInt;
    Dictionary<string, Building> buildingByName = new Dictionary<string, Building>();
    List<GameObject> buildingsInGame = new List<GameObject>();

    Animator animator;
    Grid grid;
    int maxWoodHut = 2;
    int maxStoneHut = 1;
    int maxMetalHut = 1;
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
        AIStateManager state = new AIStateManager();
        animator = GetComponent<Animator>();
        enManager = EnemyWorkerManager.Instance;
        setSpawnLocation(this.transform);
        createDictionary();
        InvokeRepeating("checkUnderAttack", 2.0f, 2.0f);
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        checkBuildingNeeded();
        checkWorkerNeeded();
    }

    void checkBuildingNeeded()
    {
        animator.SetBool("MetalHut", enManager.maxMetalWorkers >= maxMetalHut ? true : false);
        animator.SetBool("StoneHut", enManager.maxStoneWorkers >= maxStoneHut ? true : false);
        animator.SetBool("WoodHut", enManager.maxWoodWorkers >= maxWoodHut ? true : false);

    }

    void checkWorkerNeeded()
    {
        bool check;
        bool check1 = enManager.AllowWorker(ResourceType.Wood);
        bool check2 = enManager.AllowWorker(ResourceType.Metal);
        bool check3 = enManager.AllowWorker(ResourceType.Stone);
        if(check1 || check2 || check3)
            check = true; else check = false;
        animator.SetBool("worker", check);
        
    }

    void checkUnderAttack()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> inDistance = new List<GameObject>();
        foreach(GameObject o in objects)
        {
            float distance = Vector3.Distance(this.transform.position, o.transform.position);
            if(distance <= minDistanceForUnderAttack)
            {
                inDistance.Add(o);
                //AIStateManager.Instance.SetState(AIStates.UnderAttack);
            }
        }
        //if(inDistance.Count == 0) AIStateManager.Instance.SetState(AIStates.None);
    }

    public void recruitWorkers()
    {
        recruitWoodWorkers();
        recruitStoneWorkers();
        recruitMetalWorkers();
    }

    public void build(string BuildingName, Animator anim)
    {
        Building b = buildingByName[BuildingName];
        if (b != null)
        {
            b.buildingType.GetComponent<BuildingBase>().trackIfDestroyed = anim;
            build(b);
        }
        else Debug.Log("Building doesnt exist");
    }
    public void build(Building building)
    {
        int loops = 10;
        isBuild = false;
        Debug.Log("AI Building");
        BuildingObjectParameter bop = building.buildingType.GetComponent<BuildingObjectParameter>();

        if (ResourceManager.Instance.PurchaseBuilding(bop.woodCost, bop.stoneCost, PlayerTypes.AIPlayer))
        {
            while (!isBuild)
            {
                Vector3 centre = transform.position;
                Vector2 randomPos = Random.insideUnitCircle * BuildingRadius;
                Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
                RaycastHit hit;
                if (Physics.Raycast(v, Vector3.down, out hit, 20.0f))
                {
                    if (hit.collider.tag == "Ground" && !Physics.CheckBox(hit.point, DistanceBetweenBuildings, Quaternion.identity,SphereCheck) )
                    {
                        if(grid.checkNodesAreEmpty(hit.point, 3))
                        {
                        bop.playerTypes = PlayerTypes.AIPlayer;
                        building.buildingType.tag = "Enemy";
                        GameObject constructed = Instantiate(building.buildingType, hit.point, Quaternion.identity);
                        buildingsInGame.Add(constructed);
                        grid.SetNodeUnwakable(constructed);
                        isBuild = true;
                        animator.SetBool(building.name, true);
                        }
                    }
                }
                loops++;

            }
            Debug.Log("Num Loops: " + loops);
        } else animator.SetBool("missing", true);
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
            enManager.IncreaseCurrentWorkers(ResourceType.Wood);
            recruitWorkerWithTag("tree");
        }
    }

    public void recruitStoneWorkers()
    {
        if (enManager.AllowWorker(ResourceType.Stone))
        {
            enManager.IncreaseCurrentWorkers(ResourceType.Stone);
            recruitWorkerWithTag("stone");
        }
    }
    public void recruitMetalWorkers()
    {
        if (enManager.AllowWorker(ResourceType.Metal))
        {
            enManager.IncreaseCurrentWorkers(ResourceType.Metal);
            recruitWorkerWithTag("metal");
        }
    }

    private void OnDestroy()
    {
        CancelInvoke("checkUnderAttack");
    }
}
