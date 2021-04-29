using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFence : MonoBehaviour
{
    bool creating;
    public GameObject polePrefab;
    public GameObject fencePrefab;

    public float snapValue;

    GameObject lastPole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }

    public Vector3 getWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    public Vector3 snapPosition(Vector3 original, float snap)
    {
        Vector3 snapped;
        snapped.x = Mathf.Round(original.x / snap) * snap;
        snapped.y = Mathf.Round(original.y / snap) * snap;
        snapped.z = Mathf.Round(original.z / snap) * snap;
        return snapped;
    }
    void getInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startFence();
        } else if (Input.GetMouseButtonUp(0))
        {
            setFence();
        } else 
        {
            if(creating)
            {
                updateFence();
            }
        }
    }

    void startFence()
    {
        creating = true;
        Vector3 startPos = getWorldPoint();
        startPos = snapPosition(startPos, snapValue);
        GameObject startPole = Instantiate(polePrefab, startPos, Quaternion.identity);
        startPole.transform.position = new Vector3(startPos.x,startPos.y+0.3f,startPos.z);
        lastPole = startPole;
    }

    void setFence()
    {
        creating = false;
    }
    void updateFence()
    {
        Vector3 current = getWorldPoint();
        current = snapPosition(current, snapValue);
        current = new Vector3(current.x, current.y + 0.3f, current.z);
        if(!current.Equals(lastPole.transform.position))
        {
            CreateFenceSegment(current);
        }
    }

    void CreateFenceSegment(Vector3 current)
    {
        GameObject newPole = Instantiate(polePrefab, current, Quaternion.identity);
        Vector3 middle = Vector3.Lerp(newPole.transform.position, lastPole.transform.position, 0.5f);
        GameObject newFence = Instantiate(fencePrefab, middle, Quaternion.identity);
        newFence.transform.LookAt(lastPole.transform);
        lastPole = newPole;
    }
}
