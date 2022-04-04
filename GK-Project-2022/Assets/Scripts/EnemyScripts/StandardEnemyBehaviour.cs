using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class StandardEnemyBehaviour : MonoBehaviour
{
    NavMeshAgent navMesh;

    Transform player;

    [SerializeField] Collider[] goalToKill;

    //[SerializeField] int hp = 50;
    //[SerializeField] float rotSpeed = 5f;
    [SerializeField] LayerMask isPlayer, isGround;

    //walkin'
    public Vector3 goal;
    bool hasGoal;
    [SerializeField] float sightRange = 50;
    //bool hastarget = false;
    [SerializeField] float stoppingRange = 1;


    //attac
    //[SerializeField] float coolDownTime = 1;
    [SerializeField] float attackRange = 20;
    //[SerializeField] float reloadTime = 1;
    //float reloading = 1;
    //[SerializeField] bool canShoot = true;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        if (sightRange < attackRange)
            Debug.LogError("sight too short/range too long");
    }

    private void Awake()
    {
        //player = GetComponent<FirstPersonController>().transform;
    }

    void Update()
    {
        float distanceToPlayer = 999999999999;
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(player.position, transform.position);
        }
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

        navMesh.SetDestination(goal);
    }

    void Follow()
    {
        navMesh.SetDestination(player.position);
    }

    void Attack()
    {
        Debug.Log("Attacking");
    }

    void SetGoal()
    {
        Debug.Log("Setting Target");
        float randXCord = Random.Range(-sightRange, sightRange);
        float randZCord = Random.Range(-sightRange, sightRange);

        goalToKill = Physics.OverlapSphere(transform.position, sightRange, isPlayer);

        //if(goal != null)
        //{
        //    player = goalToKill[0].transform;
        //    hasGoal = true;
        //}

        //else
        {

            goal = new Vector3(transform.position.x + randXCord, 0, transform.position.z + randZCord);

            if (!Physics.Raycast(goal, -Vector3.up, 2f, isGround))
            {
                hasGoal = true;
            }
        }

    }
}
