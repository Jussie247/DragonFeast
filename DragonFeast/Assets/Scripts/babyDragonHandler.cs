using UnityEditor.TestTools.CodeCoverage;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;
using FMOD;

public enum DragonType
{
    Heal,
    Attack,
    Kamikaze
}

public class babyDragonHandler : MonoBehaviour
{
    [SerializeField] int HP = 2;
    //AI
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float followPlayerSpeed = 15, chaseEnemySpeed = 20, kamikazeSpeed = 50, patrolRange = 25;

    bool walkPointSet = false;
    Vector3 walkPoint = new Vector3();

    [SerializeField] Transform player;

    [SerializeField] LayerMask groundMask, playerMask, enemyMask;

    [SerializeField] GameObject healDragon, attackDragon, kamikazeDragon;

    public DragonType dragonType;
    GameObject levelGenerator;

    //Attacking
    [SerializeField] float ATKcooldown, explosionForce = 10;
    bool attacked;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackStartupTime, healStartupTime, kamikazeStartupTime;

    //States
    [SerializeField] float sightRange, attackRange;
    bool playerInSightRange;
    bool isBoss = false;
    bool enemyInSightRange, enemyInAttackRange;
    bool healPlayer = true, healDragons = false;

    private void Awake()
    {
        levelGenerator = GameObject.Find("LevelGenerator");
        player = GameObject.Find("RB_Based_Controller").transform;
        agent = GetComponent<NavMeshAgent>();

        //get possible dragon types, pick a random one and instance it as a child of this
        var dragonTypes = levelGenerator.GetComponent<LevelGenerationHandeler>().dragonTypes;
        // Pick a random dragon type
        int rand = UnityEngine.Random.Range(0, dragonTypes.Count);
        dragonType = dragonTypes[rand]; // cleaner and correct

        // Instantiate based on the type
        switch (dragonType)
        {
            case DragonType.Attack:
                Instantiate(attackDragon, transform);
                break;
            case DragonType.Heal:
                Instantiate(healDragon, transform);
                break;
            case DragonType.Kamikaze:
                Instantiate(kamikazeDragon, transform);
                break;
            default:
                print("Unhandled dragon type: " + dragonType);
                break;
        }
    }

    //dragon patrols when he is not near an enemy to target to in the boss room
    public void patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
        
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, groundMask))
        {
            walkPointSet = true;
        }
    }

    //dragon chases either player or enemys in the boss room
    private void chase(Transform _transform)
    {
        //set the correct animation

        agent.isStopped = false;
        //print("chasing Enemy");
        agent.SetDestination(_transform.position);

        //change chase speed based of dragon type, kamikaze goes fast when locked on to an enemy
        if (dragonType == DragonType.Heal)
        {
            agent.speed = followPlayerSpeed;
        }
        else if (dragonType == DragonType.Attack)
        {
            agent.speed = chaseEnemySpeed;
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            agent.speed = kamikazeSpeed;   
        }
    }

    private void followPlayer(Transform _transform)
    {
        agent.isStopped = false;
        //print("follow Player");
        agent.SetDestination(_transform.position);
        agent.speed = followPlayerSpeed;
    }

    //dragon attack enemy
    private void attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);

        if (!attacked)
        {
            if (dragonType == DragonType.Heal)
            {
                healCompanions();
            }
            else if (dragonType == DragonType.Attack)
            {
                enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyMask);
                if (enemyInAttackRange)
                {
                    GetClosestObjectTransformByTag("enemy").GetComponent<TestOpponent>().Hit();
                }
            }
            else if (dragonType == DragonType.Kamikaze)
            {
                enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyMask);
                if (enemyInAttackRange)
                {
                    Transform enemy = GetClosestObjectTransformByTag("enemy");
                    Destroy(enemy.GetComponent<NavMeshAgent>());
                    Rigidbody rb = enemy.AddComponent<Rigidbody>();
                    rb.AddForce(enemy.position - transform.position * explosionForce);
                    enemy.GetComponent<TestOpponent>().explosionKill();
                }
            }
            //Check if enemy is still in attack range, the hit the enemy otherwise do no dmg

            attacked = true;
            Invoke(nameof(resetAttack), ATKcooldown);
        }
    }

    private void resetAttack()
    {
        attacked = false;
    }

    private void startup()
    {
        //play the correct attack animation
        transform.GetChild(0).GetComponent<BabyDragonAnimationHandler>().playAttackAnim();
        //set the right startup time based of the dragon type
        float startupTime = 1;
        if (dragonType == DragonType.Heal)
        {
            startupTime = healStartupTime;
        }else if (dragonType == DragonType.Attack)
        {
            startupTime = attackStartupTime;
        }else if (dragonType == DragonType.Kamikaze)
        {
            startupTime = kamikazeStartupTime;
        }
        else
        {
            startupTime = attackStartupTime;
        }
        //call the attack method with startupTime as delay
        Invoke(nameof(attack), startupTime);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isBoss = levelGenerator.GetComponent<RoomManager>().isBoss;

        if(isBoss)
        {
            //Check for sight and attack range
            enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, enemyMask);
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemyMask);
            //different fight behavior based of dragon type
            if (dragonType == DragonType.Heal)
            {
                //check if Player is in sight
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
                if (playerInSightRange)
                {
                    followPlayer(player.transform);
                    //random healing to player or other babydragons if they are in range
                    startup();
                }
            }
            else if (dragonType == DragonType.Attack)
            {
                if (enemyInSightRange && enemyInAttackRange) startup();
                if (enemyInSightRange && !enemyInAttackRange)
                {
                    Transform enemy = GetClosestObjectTransformByTag("enemy");
                    chase(enemy);
                }
                if (!enemyInAttackRange && !enemyInSightRange) patrol();
            }
            else if (dragonType == DragonType.Kamikaze)
            {
                if (enemyInSightRange && enemyInAttackRange) startup();
                if (enemyInSightRange && !enemyInAttackRange)
                {
                    Transform enemy = GetClosestObjectTransformByTag("enemy");
                    chase(enemy);
                }
                if (!enemyInAttackRange && !enemyInSightRange) patrol();
            }
        }
        else
        {
            //check if Player is in sight
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            if(playerInSightRange)
            {
                followPlayer(player.transform);
                //TODO: make delta time dependant, heal after random ammount of time has passed
                if (dragonType == DragonType.Heal)
                {
                    startup();
                }
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

    public void heal()
    {
        HP++;
    }

    void healCompanions()
    {
        if (healPlayer && healDragon)
        {
            //TODO: make delta time dependant, heal after random ammount of time has passed
            int rand = Random.Range(0, 100);
            if (rand == 0)
            {
                player.GetComponent<rbBasedController>().heal(1);
                print("heal player");
            }
            else if (rand == 1)
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("BabyDragon");
                objects[Random.Range(0, objects.Length)].GetComponent<babyDragonHandler>().heal();
                print("heal enemy");
            }
        }
        else if (healPlayer)
        {
            //TODO: make delta time dependant, heal after random ammount of time has passed
            int rand = Random.Range(0, 100);
            if (rand == 0)
            {
                player.GetComponent<rbBasedController>().heal(1);
                print("heal player");
            }
        }
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
