﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{   
    public bool displayGridGizmos = true;
    public LayerMask unwakableMask;
    public float maxHeight;
    Vector3 height;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    void Awake() 
    {
        height = new Vector3(0,maxHeight,0);
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

        //CreateGrid();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            CreateGrid();
        }
    }
    public int MaxSize
    {
        get{return gridSizeX * gridSizeY;}
    }
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                RaycastHit hit;
                bool walkable = true;
                if((Physics.Raycast(worldPoint+height,-Vector3.up, out hit,20.0f)))
                {
                    if(hit.collider.tag == "water")
                    {
                        walkable = false;
                    } else walkable = true;
                }
                //(Physics.CheckSphere(worldPoint, nodeRadius,unwakableMask));
                grid[x,y] =  new Node(walkable,worldPoint,x ,y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x,y];
    }

    public void SetNodeUnwakable(GameObject gameObject)
    {
        Vector3 worldPos = gameObject.transform.position;
        Collider collider = gameObject.GetComponent<Collider>();
        Vector3 size = collider.bounds.size/2;

        for (int x = -(int)size.x; x < size.x; x++)
        {
            for (int y = -(int)size.z; y < size.z; y++)
            {
                Vector3 checkPos = worldPos + new Vector3(-x,0,y);
                Node node = NodeFromWorldPoint(checkPos);
                node.walkable = false;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <=1 ; x++)
        {
            for(int y = -1; y <=1 ; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }
        return neighbours;
    }

    void OnDrawGizmos() 
    {

        if(grid != null && displayGridGizmos)
        {
         foreach(Node n in grid)
                {
                 Gizmos.color = (n.walkable)?Color.white:Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-0.1f));
                }
        
            
            
        }
    }
}
