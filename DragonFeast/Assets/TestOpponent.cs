using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TestOpponent : MonoBehaviour
{
    public int HP = 2;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, playerMask, wallMask, enemyMask;

    public bool isBounce = false, hitWall = false;
    public GameObject radicle;
    public float punchForce = 1000;
    public int bounces = 1;

    //Attacking
    public float ATKcooldown = 200;
    public bool attacked;
    //TODO: make the enemy attack with a delay then check if the player is still in range, if he is, hit the player.
    public float attackSpeed;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("RB_Based_Controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void idle()
    {

    }

    private void chase()
    {
        agent.isStopped = false;
        //print("chasing Player");
        //agent.SetDestination(player.position);
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
        if (!isBounce)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

            if (playerInSightRange && playerInAttackRange) attack();
            if (playerInSightRange && !playerInAttackRange) chase();
            if (!playerInAttackRange && !playerInSightRange) idle();
        }

        //check if the Enemy is dead
        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        //check for bounce collisions 
        if (isBounce && hitWall)
        {
            //bounce
            HP--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make bounces configurable without imideatly deleting the game obj
        if (isBounce && bounces > 0)
        {
            // Check for Collision with a specified layer
            if ((wallMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                print("hit a wall");
                hitWall = true;
                bounces--;
            }
            if ((enemyMask.value & (1 << collision.gameObject.layer)) != 0)
            {
                print("hit another enemy");
                Transform enemy = GetClosestObjectTransformByTag("enemy");
                enemy.GetComponent<TestOpponent>().Hit();
                enemy.GetComponent<TestOpponent>().bounce();
                bounces--;
            }
        }else if(isBounce && bounces <= 0)
        {
            Hit();
        }
    }

    public void Hit()
    {
        HP--;
    }

    public void bounce()
    {
        //set to a different layer so it does not "self collide"
        transform.gameObject.tag = "bounce";

        print("bounce Mode active");
        //remove AI
        Destroy(agent);
        //add physics
        transform.gameObject.AddComponent<Rigidbody>();
        //launch the enemy
        Vector3 pointer = radicle.transform.position - player.transform.position;
        Vector3 force = pointer * punchForce;
        GetComponent<Rigidbody>().AddForce(force);
        //stop script from trying to acces AI
        isBounce = true;
        playerInAttackRange = false;
        playerInSightRange = false;
    }

    public void eat()
    {
        HP = 0;
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
