using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public float RayLenght = 10.0f;
    public GameObject[] trees;
    public GameObject[] stones;
    public GameObject[] metals;

    public float maxSample;
    public float radius = 1;
    public float spawnDistanceFromTownHall = 10.0f;
    public float TreeValue = 20;
    public float StoneValue = 25;
    public float MetalValue = 30;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSample = 30;
    public float displayRadius = 1;
    List<Vector2> points;

    GameObject[] townHalls;
    void FindBuildings()
    {
        townHalls = GameObject.FindGameObjectsWithTag("building");
    }
    public void spawnTrees()
    {
        FindBuildings();
        points = poissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSample);

        RaycastHit hit;
        foreach (Vector2 point in points)
        {
            if (Physics.Raycast(new Vector3(transform.position.x - point.x,  RayLenght, transform.position.z - point.y), -Vector3.up, out hit))
            {
                if(hit.collider.tag != "water")
                {
                    if(Vector3.Distance(hit.point, townHalls[0].transform.position) > spawnDistanceFromTownHall && Vector3.Distance(hit.point, townHalls[1].transform.position) > spawnDistanceFromTownHall)
                    {
                        GameObject spawnObject = null;
                        float value = Random.Range(0, maxSample);
                        
                        if(value <= MetalValue)
                            spawnObject = metals[Random.Range(0, metals.Length-1)];
                        if(value <= StoneValue)
                            spawnObject = stones[Random.Range(0, stones.Length-1)];
                        if(value <= TreeValue)
                            spawnObject = trees[Random.Range(0, trees.Length-1)];

                        float angle = Random.Range(0, 360);
                        GameObject obj = Instantiate(spawnObject, hit.point, Quaternion.AngleAxis(angle,Vector3.up));
                        obj.transform.parent = this.transform;
                    }
                }
            }
        }
    }

    private void OnValidate()
    {
        //points = poissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSample);

    }

    private void OnDrawGizmos()
    {
        int index = 0;
        if(points != null)
        {
            foreach (Vector2 point in points)
            {
                Debug.DrawRay(new Vector3(transform.position.x - point.x, 10, transform.position.z - point.y), -Vector3.up*10);
                Gizmos.DrawSphere(new Vector3(transform.position.x - point.x,0, transform.position.z - point.y), displayRadius);
                index++;
            }
        }
    }
}
