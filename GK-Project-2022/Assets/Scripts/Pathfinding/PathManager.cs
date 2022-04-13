using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathManager instance;
    Pathfinder pathfinder;

    bool isProcessing;

    private void Awake()
    {
        instance = this;
        pathfinder = GetComponent<Pathfinder>();
    }

    public static void RequestPath(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
    {
        PathRequest pathRequest = new PathRequest(start, end, callback);
        instance.pathRequestQueue.Enqueue(pathRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if(!isProcessing && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            pathfinder.FindPathForOne(currentPathRequest.start, currentPathRequest.end);
        }
    }

    public void FinishedProcessing(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessing = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 start;
        public Vector3 end;
        public Action<Vector3[], bool> callback;
        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            start = _start;
            end = _end;
            callback = _callback;
        }
    }

}
