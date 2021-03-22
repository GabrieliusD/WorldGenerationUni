using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequest : MonoBehaviour
{
    Queue<Path> pathRequestQueue = new Queue<Path>();
    Path currentPath;
    PathFinding pathFinding;
    bool isProcessingPath;
    static PathRequest instace;
    void Awake()
    {
        instace = this;
        pathFinding = GetComponent<PathFinding>();
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        Path newPath = new Path(pathStart, pathEnd, callback);
        instace.pathRequestQueue.Enqueue(newPath);
        instace.TryProcessNext();
    }

    public static bool RequestNonThreadedPath(Vector3 pathStart, Vector3 pathEnd)
    {
        return instace.pathFinding.StartFindingPathNonThreaded(pathStart, pathEnd);
    }

    void TryProcessNext()
    {   
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPath = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPath.pathStart, currentPath.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPath.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct Path{
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
}
