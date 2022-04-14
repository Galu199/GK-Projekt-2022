using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class StandardEnemyBehaviour : MonoBehaviour
{
    Transform player;

    //Collider[] goalToKill;

    //[SerializeField] int hp = 50;
    //[SerializeField] float rotSpeed = 5f;
    [SerializeField] LayerMask isPlayer, isGround;

    //walkin'
    public Vector3 goal;
    bool hasGoal;

    [SerializeField] float speed = 5;
    Vector3[] path;
    int pathIndex;

    [SerializeField] float sightRange = 50;
    //bool hastarget = false;
    [SerializeField] float stoppingRange = 1;


    //attac
    //[SerializeField] float coolDownTime = 1;
    [SerializeField] float attackRange = 20;
    //[SerializeField] float reloadTime = 1;
    //float reloading = 1;
    //[SerializeField] bool canShoot = true;

    void Awake()
    {
        player = GameObject.Find("FPSController").transform;
        if (sightRange < attackRange)
            Debug.LogError("sight too short/range too long");
    }

    private void Start()
    {
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > sightRange)
        {
            Wander();
        }
        if (distanceToPlayer < sightRange && distanceToPlayer > attackRange)
        {
            Follow();
        }
        if (distanceToPlayer < attackRange)
        {
            Attack();
        }
    }



    void Wander()
    {
        if (!hasGoal)
        {
            SetGoal();
        }

        Vector3 distance = transform.position - goal;

        if (distance.magnitude < stoppingRange)
        {
            hasGoal = false;
            return;
        }

        PathManager.RequestPath(transform.position, goal, OnPathFound);
    }

    void Follow()
    {
        PathManager.RequestPath(transform.position, player.position, OnPathFound);
    }

    void OnPathFound(Vector3[] newPath, bool pathSuccesfull)
    {
        if (pathSuccesfull)
        {
            path = null;
            pathIndex = 0;
            path = newPath;
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (Vector3.Distance(transform.position, goal) < stoppingRange)
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

    void Attack()
    {
        //Debug.Log("Attacking");
    }

    void SetGoal()
    {
        float randXCord = Random.Range(-sightRange, sightRange);
        float randZCord = Random.Range(-sightRange, sightRange);

        goal = new Vector3(transform.position.x + randXCord, 0, transform.position.z + randZCord);

        if (!Physics.Raycast(goal, -Vector3.up, 2f, isGround))
        {
            hasGoal = true;
        }

    }
}
