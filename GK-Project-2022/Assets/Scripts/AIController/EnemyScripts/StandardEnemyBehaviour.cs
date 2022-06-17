using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class StandardEnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Collider[] goalToKill;

    //[SerializeField] int hp = 50;
    //[SerializeField] float rotSpeed = 5f;
    [SerializeField] LayerMask isPlayer, isGround;

    //walkin'
    public bool hasGoal;
    public Vector3 goal;
    [SerializeField] float sightRange = 50;
    //bool hastarget = false;
    [SerializeField] float stoppingRange = 1;

    //attac
    //[SerializeField] float coolDownTime = 1;
    [SerializeField] float attackRange = 20;
    //[SerializeField] float reloadTime = 1;
    //float reloading = 1;
    //[SerializeField] bool canShoot = true;

    //Seweryn
    public bool wander = false;
    public bool follow = false;
    public bool attack = false;
    public double speed = 1.5;
    public double cooldown = 10;
    public double time = 0;
    public float velocity = 0;

    private void Awake()
    {
        Collider[] goalToKill = Physics.OverlapSphere(transform.position, 999999999999, isPlayer);
        if (goalToKill.Length > 0)
        {
            player = goalToKill[0].transform;
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (sightRange < attackRange)
            Debug.LogError("sight too short/range too long");
    }

    private void Update()
    {
        time += Time.deltaTime;
        velocity = agent.velocity.magnitude;

        wander = false;
        follow = false;
        attack = false;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > sightRange)
        {
            Wander();
            wander = true;
            agent.speed = (float)speed;
            if (velocity < 0.5) wander = false;
            if (time < cooldown) return;
            time = 0;
            if (velocity > 0.5) return;
            hasGoal = false;
        }
        if (distanceToPlayer < sightRange && distanceToPlayer > attackRange)
        {
            Follow();
            follow = true;
            agent.speed = (float)speed * 2;
        }
        if (distanceToPlayer < attackRange)
        {
            Attack();
            attack = true;
            agent.speed = (float)speed / 2;
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

        agent.SetDestination(goal);
    }

    void Follow()
    {
        //Debug.Log("Following");
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        //Debug.Log("Attacking");
    }

    void SetGoal()
    {
        float randXCord = Random.Range(-sightRange, sightRange);
        float randZCord = Random.Range(-sightRange, sightRange);

        goalToKill = Physics.OverlapSphere(transform.position, sightRange, isPlayer);

        if (goalToKill.Length > 0)
        {
            player = goalToKill[0].transform;
            hasGoal = true;
        }
        else
        {
            goal = new Vector3(transform.position.x + randXCord, 0, transform.position.z + randZCord);

            if (!Physics.Raycast(goal, -Vector3.up, 2f, isGround))
            {
                hasGoal = true;
            }
        }

    }
}
