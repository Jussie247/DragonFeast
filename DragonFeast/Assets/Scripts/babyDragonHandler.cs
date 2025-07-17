using UnityEditor.TestTools.CodeCoverage;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public enum DragonType
{
    Heal,
    Attack,
    Kamikaze
}

public class babyDragonHandler : MonoBehaviour
{
    [SerializeField] int HP = 2;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform player;

    [SerializeField] LayerMask groundMask, playerMask, enemyMask;

    [SerializeField] GameObject healDragon, attackDragon, kamikazeDragon;

    public DragonType dragonType;
    GameObject levelGenerator;

    //Attacking
    [SerializeField] float ATKcooldown;
    [SerializeField] bool attacked;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackStartupTime, healStartupTime, kamikazeStartupTime;

    //States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange;
    [SerializeField] bool isBoss;
    [SerializeField] bool enemyInSightRange, enemyInAttackRange;

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
                Debug.LogWarning("Unhandled dragon type: " + dragonType);
                break;
        }
    }

    //dragon patrols when he is not near an enemy to target to in the boss room
    public void patrol()
    {
        //transform.GetChild(0).GetComponent<babyDragonAnimationHandler>().idle();
    }

    //dragon chases either player or enemys in the boss room
    private void chase(Transform _transform)
    {
        //set the correct animation
        //transform.GetChild(0).GetComponent<babyDragonAnimationHandler>().idle();

        agent.isStopped = false;
        //print("chasing Player");
        agent.SetDestination(_transform.position);

        //change chase speed based of dragon type, kamikaze goes fast when locked on to an enemy
        if (dragonType == DragonType.Heal)
        {
            
        }
        else if (dragonType == DragonType.Attack)
        {
            
        }
        else if (dragonType == DragonType.Kamikaze)
        {
            
        }
    }

    //dragon attack enemy
    private void attack()
    {
        agent.isStopped = true;
        transform.LookAt(player);

        if (!attacked)
        {
            print("attacking Player");
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
        //transform.GetChild(0).GetComponent<babyDragonAnimationHandler>().idle();
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
            if (enemyInSightRange && enemyInAttackRange) attack();//only attack if the type is attack dragon
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
                chase(player.transform);// maybe add random healing to player if hes in range
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
