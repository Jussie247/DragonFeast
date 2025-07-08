using UnityEngine;
using UnityEngine.AI;

public class babyDragonHandler : MonoBehaviour
{
    public int HP = 2;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, playerMask, enemyMask;

    //Attacking
    public float ATKcooldown;
    public bool attacked;
    //TODO: make the enemy attack with a delay then check if the player is still in range, if he is, hit the player.
    public float attackSpeed;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange;
    public bool isBoss;
    public bool enemyInSightRange, enemyInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void patrol()
    {

    }

    private void chase(Transform _transform)
    {
        agent.isStopped = false;
        //print("chasing Player");
        agent.SetDestination(_transform.position);
    }

    private void attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);

        if (!attacked)
        {
            print("attacking Player");
            //player.GetComponent<rbBasedController>().hit();
            attacked = true;
            Invoke(nameof(resetAttack), ATKcooldown);
        }
    }

    private void resetAttack()
    {
        attacked = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoss)
        {
            //Check for sight and attack range
            enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyMask);
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyMask);
            if (enemyInSightRange && enemyInAttackRange) attack();
            if (enemyInSightRange && !enemyInAttackRange)
            {
                Transform enemy = GetClosestObjectTransformByTag("enemy");
                chase(enemy);
            }
            if (!enemyInAttackRange && !enemyInSightRange) patrol();
        }
        else
        {
            //check if Player is in sight
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            if(playerInSightRange)
            {
                chase(player.transform);
            }
        }

        //check if dead
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        HP--;
    }

    Transform GetClosestObjectTransformByTag(string tag)
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        // Convert GameObjects to Transforms
        Transform[] objectTransforms = new Transform[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            objectTransforms[i] = objects[i].transform;
        }

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in objectTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }
}
