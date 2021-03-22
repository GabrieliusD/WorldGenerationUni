using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuilding : MonoBehaviour
{
    bool isBuild = false;
    public LayerMask SphereCheck;
    Transform mLocation;
    public List<Building> buildingData;
    Dictionary<string, Building> buildingByName;
    List<string> buildings = new List<string>(){"hut", "mercenary"};
    void setSpawnLocation(Transform location)
    {
        mLocation = location;
    }
    void createDictionary()
    {
        for (int i = 0; i < buildingData.Count; i++)
        {
            buildingByName.Add(buildingData[i].name,buildingData[i]);
        }
    }
    private void Start()
    {
        setSpawnLocation(this.transform);
        createDictionary();
        buildResourcesCollection();
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
        if(b != null)
        build(b); else Debug.Log("Building doesnt exist");
    }
    public void build(Building building)
    {
        isBuild = false;
        Debug.Log("AI Building");
        while(!isBuild)
        {
            Vector3 centre = transform.position;
            int radius = 20;
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 v = centre + new Vector3(randomPos.x, 10, randomPos.y);
            RaycastHit hit;
            if(Physics.Raycast(v, Vector3.down, out hit,20.0f))
            {
                if(!Physics.CheckSphere(v, 4.0f, SphereCheck))
                {
                    Instantiate(building.buildingType ,hit.point + Vector3.up * 2,Quaternion.identity);
                    isBuild = true;
                }
            }
        }
    }


}
