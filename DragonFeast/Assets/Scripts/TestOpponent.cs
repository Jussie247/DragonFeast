using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[SerializeField]
public enum EnemyType
{
    Knight,
    Archer,
    Lancer
}

public class TestOpponent : MonoBehaviour
{
    [SerializeField] public EnemyType enemyType;

    [SerializeField] int HP = 2;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform player;

    [SerializeField] LayerMask groundMask, playerMask, wallMask, enemyMask;

    [SerializeField] bool isBounce = false, hitWall = false;
    [SerializeField] GameObject radicle, arrow;
    [SerializeField] float punchForce = 1000, arrowSpeed = 1000;
    [SerializeField] int bounces = 1;

    //Attacking
    [SerializeField] bool attacked;
    //startup is the time between attacking and hitting the player
    [SerializeField] float attackSpeed = 0.1f, startup = 0.625f, ATKcooldown = 200;

    //States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    [SerializeField] GameObject knighPrefab, archerPrefab, lancerPrefab;

    Transform animationHandler;

    private void Awake()
    {   
        player = GameObject.Find("RB_Based_Controller").transform;
        radicle = GameObject.Find("plane");
        arrow = GameObject.Find("arrow");
        agent = GetComponent<NavMeshAgent>();

        if (enemyType == EnemyType.Knight)
        {
            Instantiate(knighPrefab, transform.position, transform.rotation, transform);
        }
        else if (enemyType == EnemyType.Archer)
        {
            Instantiate(archerPrefab, transform.position, transform.rotation, transform);
        }
        else if (enemyType == EnemyType.Lancer)
        {
            Instantiate(lancerPrefab, transform.position, transform.rotation, transform);
        }
        animationHandler = transform.GetChild(0);
    }

    private void idle()
    {
       
    }

    private void chase()
    {
        agent.isStopped = false;
        //print("chasing Player");
        agent.SetDestination(player.position);
        animationHandler.GetComponent<enemyAnimationHandler>().walkAnim();
    }

    private void attack()
    {

        agent.isStopped = true;
        transform.LookAt(player);

        if (!attacked)
        {
            if(enemyType == EnemyType.Knight)
            {
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
                if (playerInAttackRange)
                {
                    print("attacking Player");
                    player.GetComponent<rbBasedController>().hit();
                    attacked = true;
                    Invoke(nameof(resetAttack), ATKcooldown);
                }
                else
                {
                    Invoke(nameof(resetAttack), ATKcooldown);
                }
            }
            else if (enemyType == EnemyType.Archer)
            {
                print("attacking Player");
                //player.GetComponent<rbBasedController>().hit();
                shootArrow();
                attacked = true;
                Invoke(nameof(resetAttack), ATKcooldown);
            }
            else if(enemyType == EnemyType.Lancer)
            {

            }
        }
    }

    private void startupAttack()
    {
        animationHandler.GetComponent<enemyAnimationHandler>().endWalkAnim();
        animationHandler.GetComponent<enemyAnimationHandler>().attackAnim();
        Invoke(nameof(attack), startup);
    }

    private void resetAttack()
    {
        attacked = false;
        animationHandler.GetComponent<enemyAnimationHandler>().endAttackAnim();
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

            if (playerInSightRange && playerInAttackRange) startupAttack();
            if (playerInSightRange && !playerInAttackRange && enemyType == EnemyType.Knight) chase();
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
        }
        else if (isBounce && bounces <= 0)
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

    void shootArrow()
    {
        //play arrowshooting animation
        GameObject awwow = Instantiate(arrow, transform.position + transform.forward + new Vector3(0,1,0), Quaternion.LookRotation(transform.forward));
        awwow.AddComponent<Rigidbody>();
        awwow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }
}
