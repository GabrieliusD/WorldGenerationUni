using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
public class PathRequest : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();
    PathFinding pathFinding;
    bool isProcessingPath;
    static PathRequest instace;
    void Awake()
    {
        instace = this;
        pathFinding = GetComponent<PathFinding>();
    }
    private void Update()
    {
        if(results.Count > 0)
        {
            int itemsInQueue= results.Count;
            lock(results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        Path path = new Path(pathStart, pathEnd, callback);
        ThreadStart threadStart = delegate{
            instace.pathFinding.FindPath(path, instace.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    public static bool RequestNonThreadedPath(Vector3 pathStart, Vector3 pathEnd)
    {
        return instace.pathFinding.StartFindingPathNonThreaded(pathStart, pathEnd);
    }

 

    public void FinishedProcessingPath(PathResult result)
    {
        lock(results){
        results.Enqueue(result);
        }
    }


}
    public struct PathResult
    {
        public Vector3[] path;
        public bool success;
        public Action<Vector3[], bool> callback;

        public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
        {
            this.path = path;
            this.success = success;
            this.callback = callback;
        }

    }
    public struct Path{
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public Path(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }
