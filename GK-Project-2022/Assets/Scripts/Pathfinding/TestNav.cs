using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNav : MonoBehaviour
{
    [SerializeField] Transform targetPoint;
    [SerializeField] float speed;
    Vector3[] path;
    int pathIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        PathManager.RequestPath(transform.position, targetPoint.position, OnPathFound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesfull)
    {
        if (pathSuccesfull)
        {
            path = newPath;
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                pathIndex++;
                if (pathIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[pathIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
