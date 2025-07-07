using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class TestOpponent : MonoBehaviour
{
    public int HP = 2;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, playerMask;

    //Attacking
    public float ATKcooldown;
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
        agent.SetDestination(player.position);
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
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (playerInSightRange && playerInAttackRange) attack();
        if (playerInSightRange && !playerInAttackRange) chase();
        if (!playerInAttackRange && !playerInSightRange) idle();

        //check if the Enemy is dead
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        HP--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }
}
