using UnityEngine;

public class BallsitaBossHandler : MonoBehaviour
{

    [SerializeField] private int HP = 10;
    //[SerializeField] private int arrows = 3;
    bool playerInAttackRange = false;
    float attackRange = 25;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform[] instancePositions;
    [SerializeField] GameObject arrow, egg;
    [SerializeField] float arrowSpeed, attackCooldown = 0.5f;
    bool attacked = false;

    Transform eggSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instancePositions = transform.Find("instancePosition").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfPlayerInRange();
        if(HP <= 0)
        {
            //die
            die();
        }
    }

    void checkIfPlayerInRange()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        if (playerInAttackRange)
        {
            if (!attacked)
            {
                shoot();
                attacked = true;
                Invoke(nameof(resetAttack), attackCooldown);
            }
        }
    }

    void shoot()
    {
        for (int i = 0; i < instancePositions.Length; i++)
        {
            GameObject awwow = Instantiate(arrow, instancePositions[i].position + instancePositions[i].forward + new Vector3(0, 1, 0), Quaternion.LookRotation(transform.forward));
            awwow.AddComponent<Rigidbody>();
            awwow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed);
        }
    }

    private void resetAttack()
    {
        attacked = false;
    }

    void die()
    {
        eggSpawn = GameObject.Find("eggSpawn").transform;
        Instantiate(egg, eggSpawn.position, eggSpawn.rotation);
        Destroy(transform.gameObject);
    }

    public void hit()
    {
        HP--;
    }
}
