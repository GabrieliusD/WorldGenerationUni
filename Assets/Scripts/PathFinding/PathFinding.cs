using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
public class PathFinding : MonoBehaviour
{
    PathRequest requestManager;
    Grid grid;
    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequest>();
    }


    public bool StartFindingPathNonThreaded(Vector3 startpos, Vector3 targetpos)
    {
        return FindPathNonThreaded(startpos, targetpos);
    }
    public void FindPath(Path request, Action<PathResult> callback)
    {
        bool pathSuccess = false;
        Node startNode = null;
        Node targetNode = null;
        Vector3[] wayPoints = new Vector3[0];
        try{

        
        Stopwatch sw= new Stopwatch();
        sw.Start();

        Vector3 startPos = request.pathStart;
        Vector3 targetPos = request.pathEnd;
         startNode = grid.NodeFromWorldPoint(startPos);
         targetNode = grid.NodeFromWorldPoint(targetPos);
        int tries = 0;
        while(tries < 10 && !targetNode.walkable)
        {
            Vector3 dir = (startPos - targetPos).normalized;
            targetPos = targetPos + dir * 2;

            targetNode = grid.NodeFromWorldPoint(targetPos);
            tries++;
        }

        if(startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                
                closedSet.Add(currentNode);

                if(currentNode == targetNode)
                {
                    sw.Stop();
//                    print("Path Found: " + sw.ElapsedMilliseconds + "ms");
                    pathSuccess = true;
                    break;
                }

                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if(!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost =  newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if(!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        } catch { 
        }
        if(pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
        }
        callback(new PathResult(wayPoints,pathSuccess,request.callback));

    }

    bool FindPathNonThreaded(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw= new Stopwatch();
        sw.Start();

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if(startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                
                closedSet.Add(currentNode);

                if(currentNode == targetNode)
                {
                    sw.Stop();
//                    print("Path Found: " + sw.ElapsedMilliseconds + "ms");
                    pathSuccess = true;
                    break;
                }

                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if(!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost =  newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if(!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        if(pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
        }
        return pathSuccess;
    }
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currenNode = endNode;
        while(currenNode != startNode)
        {
            path.Add(currenNode);
            currenNode = currenNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path) 
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        if(path.Count == 1)
        {
            waypoints.Add(path[0].worldPosition);
            return waypoints.ToArray();
        }


        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i-1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY)
        {
            return 14 * dstY + 10*(dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10*(dstY - dstX);
        }
    }
}
