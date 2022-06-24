using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class StandardEnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    //walkin and attacking
    public Vector3 goal;
    public bool hasgoal;
    public float stoppingRange = 1;
    public float attackRange = 20;

    public bool wander = false;
    public bool follow = false;
    public bool attack = false;

    public double speed = 1.5;
    public double cooldown = 10;
    public double time = 0;
    public float velocity = 0;

    public float sightRange = 50;
    public float sightangle = 30;
    public float sightheight = 1.0f;
    public Color meshColor = Color.red;

    public int scanFrequency = 30;
    public LayerMask layers, occlusionLayers;
    public List<GameObject> ObjectsInVision = new List<GameObject>();

    Collider[] colliders = new Collider[50];

    Mesh mesh;
    int count;
    float scanInterval;
    float scanTimer;

    private void Start()
    {
        if (sightRange < attackRange) Debug.LogError("sight too short/range too long");
        agent = GetComponent<NavMeshAgent>();
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        //PART0
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
        //PART1
        velocity = agent.velocity.magnitude;
        if (ObjectsInVision.Count <= 0)
        {
            agent.speed = (float)speed;
            wander = true;
            if (velocity < 0.1) wander = false;
            follow = false;
            attack = false;
            if (!hasgoal) SetGoal();
        }
        else
        {
            wander = false;
            follow = true;
            attack = false;
            agent.speed = (float)speed * 2;
            if (Vector3.Distance(ObjectsInVision[0].transform.position, transform.position) < attackRange)
            {
                wander = false;
                follow = false;
                attack = true;
                agent.speed = 0.0f;
            }
            SetGoal();
        }
        //PART2
        time += Time.deltaTime;
        if (time > cooldown)
        {
            time = 0;
            if (wander == false && follow == false && attack == false)
                hasgoal = false;
        }
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, sightRange);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;
        foreach (var obj in ObjectsInVision)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y < 0 || direction.y > sightheight) return false;

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > sightangle) return false;

        //origin.y += sightheight / 2;
        //dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers)) return false;

        return true;
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, sightRange, colliders, layers, QueryTriggerInteraction.Collide);
        ObjectsInVision.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                ObjectsInVision.Add(obj);
            }
        }
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomRight = Quaternion.Euler(0, sightangle, 0) * Vector3.forward * sightRange;
        Vector3 bottomLeft = Quaternion.Euler(0, -sightangle, 0) * Vector3.forward * sightRange;

        Vector3 topCenter = bottomCenter + Vector3.up * sightheight;
        Vector3 topRight = bottomRight + Vector3.up * sightheight;
        Vector3 topLeft = bottomLeft + Vector3.up * sightheight;

        int vert = 0;

        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -sightangle;
        float deltaAngle = (sightangle * 2) / segments;

        for (int i = 0; i < segments; i++)
        {
            bottomRight = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * sightRange;
            bottomLeft = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * sightRange;

            topRight = bottomRight + Vector3.up * sightheight;
            topLeft = bottomLeft + Vector3.up * sightheight;

            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void SetGoal()
    {
        //colliders = Physics.OverlapSphere(transform.position, sightRange, isPlayer);
        if (ObjectsInVision.Count <= 0)
        {
            float randXCord = Random.Range(-sightRange, sightRange);
            float randZCord = Random.Range(-sightRange, sightRange);
            goal = new Vector3(transform.position.x + randXCord, 0, transform.position.z + randZCord);
            if (!Physics.Raycast(goal, -Vector3.up, 2f, occlusionLayers))
            {
                hasgoal = true;
                agent.SetDestination(goal);
            }
        }
        else
        {
            hasgoal = true;
            goal = ObjectsInVision[0].transform.position;
            agent.SetDestination(goal);
        }
    }
}
